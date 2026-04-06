using InvoiceBroker.Domain.Entities;

namespace InvoiceBroker.Domain.Repositories;

public interface IComprobanteRepository
{
    Task AddAsync(Comprobante comprobante);
    Task<Comprobante?> GetByIdAsync(Guid id);
}
