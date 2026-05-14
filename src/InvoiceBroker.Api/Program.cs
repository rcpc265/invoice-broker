using InvoiceBroker.Application.Commands.IssueComprobante;
using InvoiceBroker.Domain.Repositories;
using InvoiceBroker.Infrastructure.Persistence;
using InvoiceBroker.Infrastructure.Persistence.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using InvoiceBroker.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFiles = new[] { "InvoiceBroker.Api.xml", "InvoiceBroker.Application.xml" };
    foreach (var xmlFile in xmlFiles)
    {
        var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (System.IO.File.Exists(xmlPath))
        {
            c.IncludeXmlComments(xmlPath);
        }
    }
});

// Opciones (Options Pattern)
builder.Services.Configure<InvoiceBroker.Infrastructure.Configuration.SunatOptions>(
    builder.Configuration.GetSection(InvoiceBroker.Infrastructure.Configuration.SunatOptions.SectionName));

// Capa de Infraestructura (Base de datos en Memoria por defecto para MVP)
builder.Services.AddDbContext<InvoiceBrokerDbContext>(options =>
    options.UseInMemoryDatabase("InvoiceBrokerDb"));
builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<InvoiceBrokerDbContext>());
builder.Services.AddScoped<IComprobanteRepository, ComprobanteRepository>();
builder.Services.AddScoped<InvoiceBroker.Application.Common.Interfaces.ISunatService, InvoiceBroker.Infrastructure.Services.MockSunatService>();
builder.Services.AddScoped<InvoiceBroker.Application.Common.Interfaces.IUbl21Generator, InvoiceBroker.Infrastructure.Services.Ubl21Generator>();
builder.Services.AddScoped<InvoiceBroker.Application.Common.Interfaces.IXmlSigner, InvoiceBroker.Infrastructure.Services.XmlSigner>();

// Capa de Aplicación (MediatR y Validadores)
builder.Services.AddApplication();

builder.Services.AddExceptionHandler<InvoiceBroker.Api.Infrastructure.ExceptionHandling.GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

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

app.UseExceptionHandler();
app.UseHttpsRedirection();

// Our Minimal API Endpoint
app.MapPost("/api/v1/comprobantes", async (IssueComprobanteCommand command, IMediator mediator) =>
{
    IssueComprobanteResult result = await mediator.Send(command);
    return Results.Ok(result);
})
.WithName("IssueComprobante")
.WithSummary("Emite un nuevo comprobante electrónico (SUNAT)")
.WithDescription("Valida y procesa un comprobante usando UBL 2.1.");

app.Run();
