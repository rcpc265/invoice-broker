using InvoiceBroker.Domain.Entities;

namespace InvoiceBroker.Application.Common.Interfaces;

public interface ISunatService
{
    Task SendAsync(Comprobante comprobante, CancellationToken cancellationToken = default);
}
