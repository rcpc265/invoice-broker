using FluentAssertions;
using InvoiceBroker.Application.Commands.IssueComprobante;
using InvoiceBroker.Domain.Entities;
using InvoiceBroker.Domain.Repositories;
using NSubstitute;

namespace InvoiceBroker.UnitTests.Application.Commands;

public class IssueComprobanteCommandHandlerTests
{
    [Fact]
    public async Task Given_ValidCommand_When_Handling_Then_CallsRepositoryAndReturnsId()
    {
        // Given
        var repositoryMock = Substitute.For<IComprobanteRepository>(); 
        var handler = new IssueComprobanteCommandHandler(repositoryMock);
        
        var command = new IssueComprobanteCommand 
        {
            Serie = "F001",
            Correlativo = "00000001",
            SubTotal = 100m
        };

        // When
        Guid result = await handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeEmpty(); 
        await repositoryMock.Received(1).AddAsync(Arg.Any<Comprobante>());
    }
}
