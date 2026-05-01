using FluentAssertions;
using InvoiceBroker.Infrastructure.Services;

namespace InvoiceBroker.UnitTests.Infrastructure.Services;

public class XmlSignerTests
{
    [Fact]
    public void Given_XmlWithoutSignature_When_SignXml_Then_InjectsSignatureBlock()
    {
        // Given
        var signer = new XmlSigner();
        string xml = "<ext:ExtensionContent></ext:ExtensionContent>";

        // When
        string signedXml = signer.SignXml(xml);

        // Then
        signedXml.Should().Contain("<Signature xmlns=\"http://www.w3.org/2000/09/xmldsig#\">");
        signedXml.Should().Contain("DummySignatureValue=");
    }
}
