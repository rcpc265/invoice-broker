using FluentAssertions;
using InvoiceBroker.Domain.Entities;
using InvoiceBroker.Domain.ValueObjects;
using InvoiceBroker.Infrastructure.Services;

namespace InvoiceBroker.UnitTests.Infrastructure.Services;

public class Ubl21GeneratorTests
{
    [Fact]
    public void Given_Comprobante_When_GenerateInvoiceXml_Then_ReturnsValidUbl21XmlString()
    {
        // Given
        var generator = new Ubl21Generator();
        var id = Guid.NewGuid();
        var comprobante = new Comprobante(id, new Serie("F001"), new Correlativo("1"), new Moneda("PEN"), new RucEmisor("20123456789"), new Monto(100m));

        // When
        string xml = generator.GenerateInvoiceXml(comprobante);

        // Then
        xml.Should().NotBeNullOrEmpty();
        xml.Should().Contain("UBLVersionID>2.1");
        xml.Should().Contain("CustomizationID>2.0");
        xml.Should().Contain("ID>F001-00000001");
        xml.Should().Contain("20123456789");
        xml.Should().Contain("currencyID=\"PEN\">100.00");
    }
}
