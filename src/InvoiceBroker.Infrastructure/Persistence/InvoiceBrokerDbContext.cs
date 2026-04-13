using InvoiceBroker.Domain.Entities;
using InvoiceBroker.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace InvoiceBroker.Infrastructure.Persistence;

public class InvoiceBrokerDbContext : DbContext
{
    public InvoiceBrokerDbContext(DbContextOptions<InvoiceBrokerDbContext> options) : base(options) { }

    public DbSet<Comprobante> Comprobantes => Set<Comprobante>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Comprobante>(entity => 
        {
            entity.HasKey(c => c.Id);
            
            // Value Conversions
            entity.Property(c => c.Serie)
                .HasConversion(
                    serie => serie.Value,
                    value => new Serie(value))
                .HasMaxLength(4);

            entity.Property(c => c.Correlativo)
                .HasConversion(
                    correlativo => correlativo.Value,
                    value => new Correlativo(value))
                .HasMaxLength(8);
        });
    }
}
