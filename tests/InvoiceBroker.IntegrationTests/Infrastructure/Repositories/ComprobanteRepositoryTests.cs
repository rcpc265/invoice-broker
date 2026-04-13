using FluentAssertions;
using InvoiceBroker.Domain.Entities;
using InvoiceBroker.Domain.ValueObjects;
using InvoiceBroker.Infrastructure.Persistence;
using InvoiceBroker.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InvoiceBroker.IntegrationTests.Infrastructure.Repositories;

public class ComprobanteRepositoryTests
{
    [Fact]
    public async Task Given_ValidComprobante_When_AddAsync_Then_SavesToDatabase()
    {
        // Given
        var options = new DbContextOptionsBuilder<InvoiceBrokerDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            
        using InvoiceBrokerDbContext dbContext = new InvoiceBrokerDbContext(options);
        ComprobanteRepository repository = new ComprobanteRepository(dbContext);
        
        Guid id = Guid.NewGuid();
        Serie serie = new Serie("F001");
        Correlativo correlativo = new Correlativo("1"); // It will pad to 00000001
        
        Comprobante comprobante = new Comprobante(id, serie, correlativo, 100m);

        // When
        await repository.AddAsync(comprobante);

        // Then
        Comprobante? savedComprobante = await dbContext.Comprobantes.FirstOrDefaultAsync();
        savedComprobante.Should().NotBeNull();
        savedComprobante!.Serie.Value.Should().Be("F001");
        savedComprobante.Correlativo.Value.Should().Be("00000001");
    }
}
