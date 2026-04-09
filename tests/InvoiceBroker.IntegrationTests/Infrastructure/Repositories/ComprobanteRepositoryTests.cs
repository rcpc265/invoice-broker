using FluentAssertions;
using InvoiceBroker.Domain.Entities;
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
            
        using var dbContext = new InvoiceBrokerDbContext(options);
        var repository = new ComprobanteRepository(dbContext);
        var comprobante = new Comprobante 
        { 
            Id = Guid.NewGuid(),
            Serie = "F001", 
            Correlativo = "00000001" 
        };

        // When
        await repository.AddAsync(comprobante);

        // Then
        var savedComprobante = await dbContext.Comprobantes.FirstOrDefaultAsync();
        savedComprobante.Should().NotBeNull();
        savedComprobante!.Serie.Should().Be("F001");
    }
}
