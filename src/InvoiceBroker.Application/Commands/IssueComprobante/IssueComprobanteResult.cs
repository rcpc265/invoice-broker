namespace InvoiceBroker.Application.Commands.IssueComprobante;

public class IssueComprobanteResult
{
    public Guid ComprobanteId { get; set; }
    public string Serie { get; set; } = string.Empty;
    public string Correlativo { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}
