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

            entity.Property(c => c.Moneda)
                .HasConversion(
                    moneda => moneda.Value,
                    value => new Moneda(value))
                .HasMaxLength(3);

            entity.Property(c => c.RucEmisor)
                .HasConversion(
                    ruc => ruc.Value,
                    value => new RucEmisor(value))
                .HasMaxLength(11);

            entity.Property(c => c.SubTotal)
                .HasConversion(
                    monto => monto.Value,
                    value => new Monto(value))
                .HasPrecision(18, 2);

            entity.Property(c => c.Igv)
                .HasConversion(
                    monto => monto.Value,
                    value => new Monto(value))
                .HasPrecision(18, 2);

            entity.Property(c => c.Total)
                .HasConversion(
                    monto => monto.Value,
                    value => new Monto(value))
                .HasPrecision(18, 2);
        });
    }
}
