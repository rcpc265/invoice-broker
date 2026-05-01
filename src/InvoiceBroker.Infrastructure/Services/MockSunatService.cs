using InvoiceBroker.Application.Common.Interfaces;
using InvoiceBroker.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace InvoiceBroker.Infrastructure.Services;

public class MockSunatService : ISunatService
{
    private readonly ILogger<MockSunatService> _logger;
    private readonly IUbl21Generator _ublGenerator;
    private readonly IXmlSigner _xmlSigner;

    public MockSunatService(ILogger<MockSunatService> logger, IUbl21Generator ublGenerator, IXmlSigner xmlSigner)
    {
        _logger = logger;
        _ublGenerator = ublGenerator;
        _xmlSigner = xmlSigner;
    }

    public async Task SendAsync(Comprobante comprobante, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("MockSunatService: Iniciando comunicación con SUNAT para {Serie}-{Correlativo}...", 
            comprobante.Serie.Value, comprobante.Correlativo.Value);

        string xmlUbl = _ublGenerator.GenerateInvoiceXml(comprobante);
        string signedXml = _xmlSigner.SignXml(xmlUbl);
        _logger.LogInformation("MockSunatService: XML generado y firmado correctamente ({Bytes} bytes)", signedXml.Length);

        // Simulamos un retraso de red (2 segundos) hacia el WS de SUNAT
        await Task.Delay(2000, cancellationToken);

        _logger.LogInformation("MockSunatService: ¡Comprobante {Serie}-{Correlativo} aceptado por SUNAT exitosamente!",
            comprobante.Serie.Value, comprobante.Correlativo.Value);
    }
}
