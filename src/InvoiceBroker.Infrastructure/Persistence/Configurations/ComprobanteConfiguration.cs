using InvoiceBroker.Domain.Entities;
using InvoiceBroker.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceBroker.Infrastructure.Persistence.Configurations;

public class ComprobanteConfiguration : IEntityTypeConfiguration<Comprobante>
{
    public void Configure(EntityTypeBuilder<Comprobante> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Serie)
            .HasConversion(
                serie => serie.Value,
                value => new Serie(value))
            .HasMaxLength(4);

        builder.Property(c => c.Correlativo)
            .HasConversion(
                correlativo => correlativo.Value,
                value => new Correlativo(value))
            .HasMaxLength(8);

        builder.Property(c => c.Moneda)
            .HasConversion(
                moneda => moneda.Value,
                value => new Moneda(value))
            .HasMaxLength(3);

        builder.Property(c => c.RucEmisor)
            .HasConversion(
                ruc => ruc.Value,
                value => new RucEmisor(value))
            .HasMaxLength(11);

        builder.Property(c => c.SubTotal)
            .HasConversion(
                monto => monto.Value,
                value => new Monto(value))
            .HasPrecision(18, 2);

        builder.Property(c => c.Igv)
            .HasConversion(
                monto => monto.Value,
                value => new Monto(value))
            .HasPrecision(18, 2);

        builder.Property(c => c.Total)
            .HasConversion(
                monto => monto.Value,
                value => new Monto(value))
            .HasPrecision(18, 2);
    }
}
