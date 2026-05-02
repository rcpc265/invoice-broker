using System.Net.Http;
using InvoiceBroker.Application.Common.Interfaces;
using InvoiceBroker.Domain.Entities;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;

namespace InvoiceBroker.Infrastructure.Services;

public class MockSunatService : ISunatService
{
    private readonly ILogger<MockSunatService> _logger;
    private readonly IUbl21Generator _ublGenerator;
    private readonly IXmlSigner _xmlSigner;
    private readonly AsyncRetryPolicy _retryPolicy;
    private readonly AsyncCircuitBreakerPolicy _circuitBreakerPolicy;

    public MockSunatService(ILogger<MockSunatService> logger, IUbl21Generator ublGenerator, IXmlSigner xmlSigner)
    {
        _logger = logger;
        _ublGenerator = ublGenerator;
        _xmlSigner = xmlSigner;

        _retryPolicy = Policy
            .Handle<HttpRequestException>()
            .Or<TimeoutException>()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (exception, timeSpan, retryCount, context) =>
                {
                    _logger.LogWarning("SUNAT Mock Falló. Reintento {RetryCount} en {Delay}s debido a: {Message}",
                        retryCount, timeSpan.TotalSeconds, exception.Message);
                });

        _circuitBreakerPolicy = Policy
            .Handle<HttpRequestException>()
            .Or<TimeoutException>()
            .CircuitBreakerAsync(2, TimeSpan.FromSeconds(30),
                (exception, duration) =>
                {
                    _logger.LogError("Circuit Breaker OPEN durante {Duration}s debido a: {Message}", duration.TotalSeconds, exception.Message);
                },
                () =>
                {
                    _logger.LogInformation("Circuit Breaker CLOSED");
                });
    }

    public async Task SendAsync(Comprobante comprobante, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("MockSunatService: Iniciando comunicación con SUNAT para {Serie}-{Correlativo}...", 
            comprobante.Serie.Value, comprobante.Correlativo.Value);

        string xmlUbl = _ublGenerator.GenerateInvoiceXml(comprobante);
        string signedXml = _xmlSigner.SignXml(xmlUbl);
        _logger.LogInformation("MockSunatService: XML generado y firmado correctamente ({Bytes} bytes)", signedXml.Length);

        // Envuelve la ejecución simulada con Polly
        await Policy.WrapAsync(_retryPolicy, _circuitBreakerPolicy).ExecuteAsync(async () =>
        {
            // Simulamos un fallo aleatorio (20% de probabilidad)
            if (Random.Shared.Next(0, 5) == 0)
            {
                throw new TimeoutException("Simulated network timeout connecting to SUNAT WS");
            }

            // Simulamos un retraso de red normal (1 segundo)
            await Task.Delay(1000, cancellationToken);
        });

        _logger.LogInformation("MockSunatService: ¡Comprobante {Serie}-{Correlativo} aceptado por SUNAT exitosamente!",
            comprobante.Serie.Value, comprobante.Correlativo.Value);
    }
}
