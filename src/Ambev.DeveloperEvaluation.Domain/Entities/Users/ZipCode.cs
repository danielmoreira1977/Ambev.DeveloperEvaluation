using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Users
{
    [Owned]
    public sealed class ZipCode(string value)
    {
        public string Value { get; init; } = value;

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(Value))
            {
                return string.Empty;
            }

            var cep = new string([.. Value.Where(char.IsDigit)]);

            if (cep.Length != 8)
            {
                return cep; // Retorna como está se não tiver 8 dígitos
            }

            return $"{cep[..5]}-{cep.Substring(5, 3)}";
        }
    }
}
