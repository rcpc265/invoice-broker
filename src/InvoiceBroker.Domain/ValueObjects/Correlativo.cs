using System.Text.RegularExpressions;

namespace InvoiceBroker.Domain.ValueObjects;

public record Correlativo
{
    public string Value { get; }

    public Correlativo(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El correlativo no puede ser nulo ni estar vacío.");

        // SUNAT Rule: Only numbers, 1 to 8 digits max. 
        // We apply Camino A (Normalization): padding with zeros to ensure 8 digits.
        if (!Regex.IsMatch(value, "^[0-9]{1,8}$"))
            throw new ArgumentException($"El formato del Correlativo '{value}' es inválido. Debe ser numérico y tener hasta 8 dígitos.");

        Value = value.PadLeft(8, '0');
    }
}
