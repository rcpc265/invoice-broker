using FluentAssertions;
using InvoiceBroker.Domain.Entities;

namespace InvoiceBroker.UnitTests.Domain.Entities;

public class ComprobanteTests
{
    [Fact]
    public void Given_ComprobanteWithSubTotal_When_CalculateTotals_Then_IgvAndTotalAreCorrectlyCalculated()
    {
        // Given
        var comprobante = new Comprobante
        {
            Id = Guid.NewGuid(),
            Serie = "F001",
            Correlativo = "00000001",
            SubTotal = 100m
        };

        // When
        comprobante.CalculateTotals();

        // Then
        comprobante.Igv.Should().Be(18m);
        comprobante.Total.Should().Be(118m);
    }
}
