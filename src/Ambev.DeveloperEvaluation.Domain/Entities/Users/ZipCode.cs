using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Users
{
    [Owned]
    public sealed class ZipCode()
    {
        public string Value { get; set; } = string.Empty;

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(Value))
            {
                return string.Empty;
            }

            var cep = new string(Value.Where(char.IsDigit).ToArray());

            if (cep.Length != 8)
            {
                return cep; // Retorna como está se não tiver 8 dígitos
            }

            return $"{cep[..5]}-{cep.Substring(5, 3)}";
        }
    }
}
