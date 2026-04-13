using FluentAssertions;
using InvoiceBroker.Domain.Entities;
using InvoiceBroker.Domain.ValueObjects;

namespace InvoiceBroker.UnitTests.Domain.Entities;

public class ComprobanteTests
{
    [Fact]
    public void Given_ComprobanteWithSubTotal_When_Created_Then_IgvAndTotalAreCorrectlyCalculated()
    {
        // Given
        Guid id = Guid.NewGuid();
        Serie serie = new Serie("F001");
        Correlativo correlativo = new Correlativo("1");
        decimal subTotal = 100m;

        // When
        Comprobante comprobante = new Comprobante(id, serie, correlativo, subTotal);

        // Then
        comprobante.Igv.Should().Be(18m);
        comprobante.Total.Should().Be(118m);
    }
}
