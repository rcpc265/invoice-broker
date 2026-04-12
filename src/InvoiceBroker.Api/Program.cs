using InvoiceBroker.Application.Commands.IssueComprobante;
using InvoiceBroker.Domain.Repositories;
using InvoiceBroker.Infrastructure.Persistence;
using InvoiceBroker.Infrastructure.Persistence.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

// Register Infrastructure
builder.Services.AddDbContext<InvoiceBrokerDbContext>(options =>
{
    // Usaremos un connection string temporal. En el futuro usaremos appsettings.json y secretos
    options.UseSqlServer("Server=.;Database=InvoiceBrokerDb;Trusted_Connection=True;TrustServerCertificate=True;");
});

builder.Services.AddScoped<IComprobanteRepository, ComprobanteRepository>();

// Register Application (MediatR)
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(IssueComprobanteCommand).Assembly);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Our Minimal API Endpoint
app.MapPost("/api/comprobantes", async (IssueComprobanteCommand command, IMediator mediator) =>
{
    Guid id = await mediator.Send(command);
    return Results.Ok(new { Id = id });
})
.WithName("IssueComprobante");

app.Run();
