namespace InvoiceBroker.Domain.ValueObjects;

public record Moneda
{
    public string Value { get; }

    public Moneda(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length != 3)
            throw new ArgumentException("Moneda must be exactly 3 characters (e.g., PEN).", nameof(value));

        Value = value;
    }
}
