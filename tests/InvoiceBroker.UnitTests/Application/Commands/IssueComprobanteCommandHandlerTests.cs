using FluentAssertions;
using InvoiceBroker.Application.Commands.IssueComprobante;
using InvoiceBroker.Application.Common.Interfaces;
using InvoiceBroker.Domain.Entities;
using InvoiceBroker.Domain.Repositories;
using NSubstitute;

namespace InvoiceBroker.UnitTests.Application.Commands;

public class IssueComprobanteCommandHandlerTests
{
    [Fact]
    public async Task Given_ValidCommand_When_Handling_Then_CallsRepositoryAndSunatService()
    {
        // Given
        var repositoryMock = Substitute.For<IComprobanteRepository>(); 
        var unitOfWorkMock = Substitute.For<IUnitOfWork>();
        repositoryMock.UnitOfWork.Returns(unitOfWorkMock);

        var sunatMock = Substitute.For<ISunatService>();
        var handler = new IssueComprobanteCommandHandler(repositoryMock, sunatMock);
        
        var command = new IssueComprobanteCommand 
        {
            Serie = "F001",
            Correlativo = "00000001",
            SubTotal = 100m,
            Moneda = "PEN",
            RucEmisor = "20123456789"
        };

        // When
        var result = await handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.ComprobanteId.Should().NotBeEmpty();
        result.Serie.Should().Be("F001");
        await repositoryMock.Received(1).AddAsync(Arg.Any<Comprobante>());
        await sunatMock.Received(1).SendAsync(Arg.Any<Comprobante>(), Arg.Any<CancellationToken>());
    }
}
