using System.Text.RegularExpressions;

namespace InvoiceBroker.Domain.ValueObjects;

public record Serie
{
    public string Value { get; }

    public Serie(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Serie cannot be null or empty.");

        // SUNAT Rule: Starts with F, B, E, or T followed by 3 alphanumeric chars.
        if (!Regex.IsMatch(value, "^[FBET][A-Z0-9]{3}$"))
            throw new ArgumentException($"Serie '{value}' format is invalid. Must be 4 chars starting with F, B, E or T (e.g., F001).");

        Value = value;
    }
}
