using System.ComponentModel;
using MediatR;

namespace InvoiceBroker.Application.Commands.IssueComprobante;

public class IssueComprobanteCommand : IRequest<Guid>
{
    [DefaultValue("F001")]
    public string Serie { get; set; } = string.Empty;

    [DefaultValue("1")]
    public string Correlativo { get; set; } = string.Empty;

    [DefaultValue(100.50)]
    public decimal SubTotal { get; set; }
}
