using System.Text.RegularExpressions;

namespace InvoiceBroker.Domain.ValueObjects;

public record Serie
{
    public string Value { get; }

    public Serie(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("La serie no puede ser nula ni estar vacía.");

        // SUNAT Rule: Starts with F, B, E, or T followed by 3 alphanumeric chars.
        if (!Regex.IsMatch(value, "^[FBET][A-Z0-9]{3}$"))
            throw new ArgumentException($"El formato de la Serie '{value}' es inválido. Debe tener 4 caracteres y empezar con F, B, E o T (ej. F001).");

        Value = value;
    }
}
