using InvoiceBroker.Domain.Entities;

namespace InvoiceBroker.Domain.Repositories;

public interface IComprobanteRepository
{
    IUnitOfWork UnitOfWork { get; }
    Task AddAsync(Comprobante comprobante);
    Task<Comprobante?> GetByIdAsync(Guid id);
}
