namespace InvoiceBroker.Domain.Entities;

public class Comprobante
{
    public Guid Id { get; set; }
    public string Serie { get; set; } = string.Empty;
    public string Correlativo { get; set; } = string.Empty;
    public decimal SubTotal { get; set; }
    public decimal Igv { get; private set; }
    public decimal Total { get; private set; }

    private const decimal IGV_RATE = 0.18m;

    public void CalculateTotals()
    {
        Igv = SubTotal * IGV_RATE;
        Total = SubTotal + Igv;
    }
}
