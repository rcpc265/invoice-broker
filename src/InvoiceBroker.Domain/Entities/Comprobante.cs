using InvoiceBroker.Domain.ValueObjects;

namespace InvoiceBroker.Domain.Entities;

public class Comprobante
{
    public Guid Id { get; private set; }
    public Serie Serie { get; private set; }
    public Correlativo Correlativo { get; private set; }
    public decimal SubTotal { get; private set; }
    public decimal Igv { get; private set; }
    public decimal Total { get; private set; }

    private const decimal IGV_RATE = 0.18m;

    // EF Core requiere un constructor sin parámetros privado
    private Comprobante() 
    { 
        Serie = null!;
        Correlativo = null!;
    }

    public Comprobante(Guid id, Serie serie, Correlativo correlativo, decimal subTotal)
    {
        Id = id;
        Serie = serie;
        Correlativo = correlativo;
        SubTotal = subTotal;
        CalculateTotals();
    }

    private void CalculateTotals()
    {
        Igv = SubTotal * IGV_RATE;
        Total = SubTotal + Igv;
    }
}
