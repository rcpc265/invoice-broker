namespace InvoiceBroker.Domain.ValueObjects;

public record RucEmisor
{
    public string Value { get; }

    public RucEmisor(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length != 11 || !value.All(char.IsDigit))
            throw new ArgumentException("RucEmisor must be exactly 11 digits.", nameof(value));

        Value = value;
    }
}
