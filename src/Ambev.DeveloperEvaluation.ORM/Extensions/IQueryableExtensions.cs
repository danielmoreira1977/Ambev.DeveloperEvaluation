using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.ORM.Extensions;

public static class IQueryableExtensions
{
    public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, string orderString)
    {
        if (string.IsNullOrWhiteSpace(orderString))
        {
            return query;
        }

        var orderClauses = orderString.Split(',');

        var validOrderClauses = new List<string>();
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                  .Select(p => p.Name.ToLower());

        foreach (var clause in orderClauses)
        {
            var parts = clause.Trim().Split(' ');
            var basePropName = parts[0].Trim().ToLower();
            var propName = Regex.Replace(basePropName, "[^a-zA-Z0-9]", "");

            var baseDirection = parts.Length > 1 ? parts[1].Trim().ToLower() : "asc";
            var direction = Regex.Replace(baseDirection, "[^a-zA-Z0-9]", "");

            if (properties.Contains(propName) && (direction == "asc" || direction == "desc"))
            {
                validOrderClauses.Add($"{parts[0]} {direction}");
            }
        }

        if (validOrderClauses.Count == 0)
        {
            return query;
        }

        var fullOrder = string.Join(", ", validOrderClauses);
        return query.OrderBy(fullOrder);
    }

    public static IQueryable<T> ApplyParameteres<T>(this IQueryable<T> query, Dictionary<string, string>? parameteres)
    {
        if (parameteres is null)
        {
            return query;
        }

        var validParameteres =
            parameteres
                .Where(kv => !kv.Key.StartsWith("_"))
                .ToDictionary(kv => kv.Key, kv => kv.Value);

        foreach (var parameter in validParameteres)
        {
            var propertyName = parameter.Key.Trim().ToLower();
            var propertyValue = parameter.Value.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(propertyName) || string.IsNullOrWhiteSpace(propertyValue))
            {
                continue;
            }

            var validPropertyName = Capitalize(propertyName);

            var propertyInfo = typeof(T).GetProperty(validPropertyName, BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo == null)
            {
                continue;
            }

            //_min int Date
            //_max int Date

            //field=value
            //title=value*
            //title=*value

            var parameterType = propertyInfo.PropertyType;

            if (parameterType == typeof(string))
            {
                if (propertyValue.StartsWith("*"))
                {
                    query = query.Where($"{propertyName}.ToLower().EndsWith(@0)", propertyValue.Replace("*", ""));
                }
                else if (propertyValue.EndsWith("*"))
                {
                    query = query.Where($"{propertyName}.ToLower().StartsWith(@0)", propertyValue.Replace("*", ""));
                }
                else
                {
                    query = query.Where($"{propertyName}.ToLower().Contains(@0)", propertyValue.Replace("*", ""));
                }
            }
            else if (parameterType == typeof(int) || parameterType == typeof(int?))
            {
                if (int.TryParse(propertyValue, out var intValue))
                {
                    query = query.Where($"{propertyName} == @0", intValue);
                }
            }
        }

        return query;
    }

    private static string Capitalize(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return char.ToUpper(input[0]) + input.Substring(1);
    }
}
