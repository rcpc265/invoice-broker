using InvoiceBroker.Application.Common.Interfaces;
using InvoiceBroker.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace InvoiceBroker.Infrastructure.Services;

public class MockSunatService : ISunatService
{
    private readonly ILogger<MockSunatService> _logger;

    public MockSunatService(ILogger<MockSunatService> logger)
    {
        _logger = logger;
    }

    public async Task SendAsync(Comprobante comprobante, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("MockSunatService: Iniciando comunicación con SUNAT para {Serie}-{Correlativo}...", 
            comprobante.Serie.Value, comprobante.Correlativo.Value);

        // Simulamos un retraso de red (2 segundos) hacia el WS de SUNAT
        await Task.Delay(2000, cancellationToken);

        _logger.LogInformation("MockSunatService: ¡Comprobante {Serie}-{Correlativo} aceptado por SUNAT exitosamente!",
            comprobante.Serie.Value, comprobante.Correlativo.Value);
    }
}
