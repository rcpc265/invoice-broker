using InvoiceBroker.Domain.ValueObjects;

namespace InvoiceBroker.Domain.Entities;

public class Comprobante
{
    public Guid Id { get; private set; }
    public Serie Serie { get; private set; }
    public Correlativo Correlativo { get; private set; }
    public Moneda Moneda { get; private set; }
    public RucEmisor RucEmisor { get; private set; }
    public Monto SubTotal { get; private set; }
    public Monto Igv { get; private set; }
    public Monto Total { get; private set; }

    private const decimal IGV_RATE = 0.18m;

    // EF Core requiere un constructor sin parámetros privado
    private Comprobante() 
    { 
        Serie = null!;
        Correlativo = null!;
        Moneda = null!;
        RucEmisor = null!;
        SubTotal = null!;
        Igv = null!;
        Total = null!;
    }

    public Comprobante(Guid id, Serie serie, Correlativo correlativo, Moneda moneda, RucEmisor rucEmisor, Monto subTotal)
    {
        Id = id;
        Serie = serie;
        Correlativo = correlativo;
        Moneda = moneda;
        RucEmisor = rucEmisor;
        SubTotal = subTotal;
        CalculateTotals();
    }

    private void CalculateTotals()
    {
        Igv = SubTotal * IGV_RATE;
        Total = SubTotal + Igv;
    }
}
