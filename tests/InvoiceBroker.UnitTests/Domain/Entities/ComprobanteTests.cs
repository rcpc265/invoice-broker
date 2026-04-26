using FluentAssertions;
using InvoiceBroker.Domain.Entities;
using InvoiceBroker.Domain.ValueObjects;

namespace InvoiceBroker.UnitTests.Domain.Entities;

public class ComprobanteTests
{
    [Fact]
    public void Given_ValidPrimitives_When_Created_Then_IgvAndTotalAreCorrectlyCalculated()
    {
        // Given
        Guid id = Guid.NewGuid();
        Serie serie = new Serie("F001");
        Correlativo correlativo = new Correlativo("1");
        Moneda moneda = new Moneda("PEN");
        RucEmisor rucEmisor = new RucEmisor("20123456789");
        Monto subTotal = new Monto(100m);

        // When
        Comprobante comprobante = new Comprobante(id, serie, correlativo, moneda, rucEmisor, subTotal);

        // Then
        comprobante.Igv.Value.Should().Be(18m);
        comprobante.Total.Value.Should().Be(118m);
        comprobante.Moneda.Value.Should().Be("PEN");
        comprobante.RucEmisor.Value.Should().Be("20123456789");
    }
}
