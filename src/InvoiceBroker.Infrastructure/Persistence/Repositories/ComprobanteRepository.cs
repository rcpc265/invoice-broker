using InvoiceBroker.Domain.Entities;
using InvoiceBroker.Domain.Repositories;

namespace InvoiceBroker.Infrastructure.Persistence.Repositories;

public class ComprobanteRepository : IComprobanteRepository
{
    private readonly InvoiceBrokerDbContext _dbContext;

    public ComprobanteRepository(InvoiceBrokerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IUnitOfWork UnitOfWork => _dbContext;

    public async Task AddAsync(Comprobante comprobante)
    {
        await _dbContext.Comprobantes.AddAsync(comprobante);
    }

    public async Task<Comprobante?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Comprobantes.FindAsync(id);
    }
}
