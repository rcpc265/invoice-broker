using FluentAssertions;
using InvoiceBroker.Domain.ValueObjects;

namespace InvoiceBroker.UnitTests.Domain.ValueObjects;

public class RucEmisorTests
{
    [Fact]
    public void Given_ValidRuc_When_Created_Then_ValueIsSet()
    {
        // Given
        string validRuc = "20123456789";

        // When
        RucEmisor rucEmisor = new RucEmisor(validRuc);

        // Then
        rucEmisor.Value.Should().Be("20123456789");
    }

    [Theory]
    [InlineData("")]
    [InlineData("1234567890")]
    [InlineData("2012345678A")]
    [InlineData(null)]
    public void Given_InvalidRuc_When_Created_Then_ThrowsArgumentException(string invalidRuc)
    {
        // Given
        // When
        Action action = () => new RucEmisor(invalidRuc!);

        // Then
        action.Should().Throw<ArgumentException>();
    }
}
