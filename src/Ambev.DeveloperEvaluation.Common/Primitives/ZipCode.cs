namespace Ambev.DeveloperEvaluation.Common.Primitives
{
    public readonly record struct ZipCode(string Value)
    {
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

// "address": { "city": "string", "street": "string", "number": "integer", "zipcode": "string",
// "geolocation": { "lat": "string", "long": "string" }
