using InvoiceBroker.Domain.Entities;
using InvoiceBroker.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

using InvoiceBroker.Domain.Repositories;

namespace InvoiceBroker.Infrastructure.Persistence;

public class InvoiceBrokerDbContext : DbContext, IUnitOfWork
{
    public InvoiceBrokerDbContext(DbContextOptions<InvoiceBrokerDbContext> options) : base(options) { }

    public DbSet<Comprobante> Comprobantes => Set<Comprobante>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InvoiceBrokerDbContext).Assembly);
    }
}
