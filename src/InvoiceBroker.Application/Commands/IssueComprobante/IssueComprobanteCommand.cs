using MediatR;

namespace InvoiceBroker.Application.Commands.IssueComprobante;

public class IssueComprobanteCommand : IRequest<Guid>
{
    public string Serie { get; set; } = "F001";
    public string Correlativo { get; set; } = "1";
    public decimal SubTotal { get; set; } = 100.00m;
}
