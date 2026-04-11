using MediatR;

namespace InvoiceBroker.Application.Commands.IssueComprobante;

public class IssueComprobanteCommand : IRequest<Guid>
{
    public string Serie { get; set; } = string.Empty;
    public string Correlativo { get; set; } = string.Empty;
    public decimal SubTotal { get; set; }
}
