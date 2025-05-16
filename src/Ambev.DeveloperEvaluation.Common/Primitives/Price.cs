namespace Ambev.DeveloperEvaluation.Common.Primitives;

//public record struct Price(decimal Value)
//{
//    public static Price Empty() => new(0m);
//}

public readonly record struct Price(decimal Value)
    : IComparable<Price>, IComparable<decimal>, IEquatable<Price>
{
    public static Price Empty() => new(0m);

    // Conversões implícitas
    public static implicit operator Price(decimal value) => new(value);
    public static implicit operator decimal(Price price) => price.Value;

    // Operadores entre Price e decimal
    public static bool operator >(Price left, decimal right) => left.Value > right;
    public static bool operator <(Price left, decimal right) => left.Value < right;
    public static bool operator >=(Price left, decimal right) => left.Value >= right;
    public static bool operator <=(Price left, decimal right) => left.Value <= right;

    public static bool operator >(decimal left, Price right) => left > right.Value;
    public static bool operator <(decimal left, Price right) => left < right.Value;
    public static bool operator >=(decimal left, Price right) => left >= right.Value;
    public static bool operator <=(decimal left, Price right) => left <= right.Value;

    // Operadores entre Price e Price
    public static bool operator >(Price left, Price right) => left.Value > right.Value;
    public static bool operator <(Price left, Price right) => left.Value < right.Value;
    public static bool operator >=(Price left, Price right) => left.Value >= right.Value;
    public static bool operator <=(Price left, Price right) => left.Value <= right.Value;

    // Implementações de interfaces
    public int CompareTo(Price other) => Value.CompareTo(other.Value);
    public int CompareTo(decimal other) => Value.CompareTo(other);
    public bool Equals(Price other) => Value == other.Value;

    // Sobrescrita de ToString (opcional)
    public override string ToString() => Value.ToString("C"); // Formato monetário
}
