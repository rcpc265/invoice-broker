namespace InvoiceBroker.Domain.ValueObjects;

public record Monto
{
    public decimal Value { get; }

    public Monto(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("Monto cannot be negative.", nameof(value));

        Value = value;
    }

    public static Monto operator +(Monto a, Monto b) => new Monto(a.Value + b.Value);
    public static Monto operator *(Monto a, decimal multiplier) => new Monto(a.Value * multiplier);
}
