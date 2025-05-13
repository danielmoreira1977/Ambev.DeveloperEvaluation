using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.Common.Primitives
{
    public readonly record struct Username
    {
        private readonly Regex _regex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

        public string Value { get; }

        public Username(string value)
        {
            //TODO Error Messages should be localized
            if (!_regex.IsMatch(value))
            {
                throw new ArgumentException("Invalid email address.", nameof(value));
            }

            Value = value;
        }
    }
}
