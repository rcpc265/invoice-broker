using InvoiceBroker.Application.Commands.IssueComprobante;
using InvoiceBroker.Domain.Repositories;
using InvoiceBroker.Infrastructure.Persistence;
using InvoiceBroker.Infrastructure.Persistence.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// Capa de Infraestructura (Base de datos en Memoria por defecto para MVP)
builder.Services.AddDbContext<InvoiceBrokerDbContext>(options =>
    options.UseInMemoryDatabase("InvoiceBrokerDb"));
builder.Services.AddScoped<IComprobanteRepository, ComprobanteRepository>();

// Capa de Aplicación (MediatR)
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IssueComprobanteCommand).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Generación de JSON OpenAPI (Nativo)
    app.MapOpenApi();
    
    // UI 1: Scalar (El moderno)
    app.MapScalarApiReference();

    // UI 2: Swagger Clásico (El viejo confiable)
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/openapi/v1.json", "InvoiceBroker API");
    });
}

app.UseHttpsRedirection();

// Our Minimal API Endpoint
app.MapPost("/api/comprobantes", async (IssueComprobanteCommand command, IMediator mediator) =>
{
    Guid id = await mediator.Send(command);
    return Results.Ok(new { Id = id });
})
.WithName("IssueComprobante")
.WithOpenApi(operation =>
{
    // Autocompletado funcional para Swagger y Scalar
    operation.Summary = "Emite un nuevo comprobante electrónico (SUNAT)";
    operation.Description = "Valida y procesa un comprobante usando UBL 2.1.";
    
    if (operation.RequestBody?.Content.TryGetValue("application/json", out var content) == true)
    {
        // En .NET 9/10 OpenAPI usa System.Text.Json.Nodes
        content.Example = System.Text.Json.Nodes.JsonNode.Parse("""
        {
            "serie": "F001",
            "correlativo": "1",
            "subTotal": 1000.50
        }
        """);
    }
    return operation;
});

app.Run();
