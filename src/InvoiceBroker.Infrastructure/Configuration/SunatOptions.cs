namespace InvoiceBroker.Infrastructure.Configuration;

public class SunatOptions
{
    public const string SectionName = "Sunat";

    public string EndpointUrl { get; set; } = string.Empty;
    public int RetryCount { get; set; } = 3;
    public int CircuitBreakerThreshold { get; set; } = 2;
    public int CircuitBreakerDurationSeconds { get; set; } = 30;
}
