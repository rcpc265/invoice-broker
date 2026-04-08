using InvoiceBroker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoiceBroker.Infrastructure.Persistence;

public class InvoiceBrokerDbContext : DbContext
{
    public InvoiceBrokerDbContext(DbContextOptions<InvoiceBrokerDbContext> options) : base(options) { }

    public DbSet<Comprobante> Comprobantes => Set<Comprobante>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Aquí configuraremos cómo se mapea el Dominio a la BD más adelante.
    }
}
