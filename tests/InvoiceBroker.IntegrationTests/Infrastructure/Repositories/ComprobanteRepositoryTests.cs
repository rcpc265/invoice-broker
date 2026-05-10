using FluentAssertions;
using InvoiceBroker.Domain.Entities;
using InvoiceBroker.Domain.ValueObjects;
using InvoiceBroker.Infrastructure.Persistence;
using InvoiceBroker.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InvoiceBroker.IntegrationTests.Infrastructure.Repositories;

public class ComprobanteRepositoryTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _fixture;

    public ComprobanteRepositoryTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Given_ValidComprobante_When_AddAsync_Then_SavesToDatabase()
    {
        // Given
        var options = new DbContextOptionsBuilder<InvoiceBrokerDbContext>()
            .UseSqlServer(_fixture.ConnectionString)
            .Options;
            
        using InvoiceBrokerDbContext dbContext = new InvoiceBrokerDbContext(options);
        
        // Fix: EF Core needs to create the schema in the SQL Server container
        await dbContext.Database.EnsureCreatedAsync();
        
        ComprobanteRepository repository = new ComprobanteRepository(dbContext);
        
        Guid id = Guid.NewGuid();
        Serie serie = new Serie("F001");
        Correlativo correlativo = new Correlativo("1"); // It will pad to 00000001
        Moneda moneda = new Moneda("PEN");
        RucEmisor rucEmisor = new RucEmisor("20123456789");
        Monto subTotal = new Monto(100m);
        
        Comprobante comprobante = new Comprobante(id, serie, correlativo, moneda, rucEmisor, subTotal);

        // When
        await repository.AddAsync(comprobante);
        await repository.UnitOfWork.SaveChangesAsync();

        // Then
        Comprobante? savedComprobante = await dbContext.Comprobantes.FirstOrDefaultAsync();
        savedComprobante.Should().NotBeNull();
        savedComprobante!.Serie.Value.Should().Be("F001");
        savedComprobante.Correlativo.Value.Should().Be("00000001");
    }
}
