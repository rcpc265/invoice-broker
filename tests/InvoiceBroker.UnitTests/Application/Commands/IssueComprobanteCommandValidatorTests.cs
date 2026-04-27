using FluentAssertions;
using InvoiceBroker.Application.Commands.IssueComprobante;

namespace InvoiceBroker.UnitTests.Application.Commands;

public class IssueComprobanteCommandValidatorTests
{
    private readonly IssueComprobanteCommandValidator _validator;

    public IssueComprobanteCommandValidatorTests()
    {
        _validator = new IssueComprobanteCommandValidator();
    }

    [Fact]
    public void Given_ValidCommand_When_Validated_Then_NoErrors()
    {
        // Given
        var command = new IssueComprobanteCommand
        {
            Serie = "F001",
            Correlativo = "00000001",
            Moneda = "PEN",
            RucEmisor = "20123456789",
            SubTotal = 100m
        };

        // When
        var result = _validator.Validate(command);

        // Then
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Given_InvalidSerie_When_Validated_Then_ReturnsError()
    {
        // Given
        var command = new IssueComprobanteCommand
        {
            Serie = "F00", // Invalid length
            Correlativo = "00000001",
            Moneda = "PEN",
            RucEmisor = "20123456789",
            SubTotal = 100m
        };

        // When
        var result = _validator.Validate(command);

        // Then
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Serie");
    }
}
