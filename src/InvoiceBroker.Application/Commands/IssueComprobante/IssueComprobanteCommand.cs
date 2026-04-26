using MediatR;

namespace InvoiceBroker.Application.Commands.IssueComprobante;

public class IssueComprobanteCommand : IRequest<Guid>
{
    /// <summary>Serie del comprobante.</summary>
    /// <example>F001</example>
    public string Serie { get; set; } = string.Empty;

    /// <summary>Número correlativo.</summary>
    /// <example>1</example>
    public string Correlativo { get; set; } = string.Empty;

    /// <summary>Subtotal (Valor de Venta) sin IGV.</summary>
    /// <example>100.50</example>
    public decimal SubTotal { get; set; }

    /// <summary>Moneda del comprobante.</summary>
    /// <example>PEN</example>
    public string Moneda { get; set; } = string.Empty;

    /// <summary>RUC del emisor.</summary>
    /// <example>20123456789</example>
    public string RucEmisor { get; set; } = string.Empty;
}
