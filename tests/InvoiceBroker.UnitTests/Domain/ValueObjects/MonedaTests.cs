using FluentAssertions;
using InvoiceBroker.Domain.ValueObjects;

namespace InvoiceBroker.UnitTests.Domain.ValueObjects;

public class MonedaTests
{
    [Fact]
    public void Given_ValidMoneda_When_Created_Then_ValueIsSet()
    {
        // Given
        string validMoneda = "PEN";

        // When
        Moneda moneda = new Moneda(validMoneda);

        // Then
        moneda.Value.Should().Be("PEN");
    }

    [Theory]
    [InlineData("")]
    [InlineData("PE")]
    [InlineData("PENN")]
    [InlineData(null)]
    public void Given_InvalidMoneda_When_Created_Then_ThrowsArgumentException(string invalidMoneda)
    {
        // Given
        // When
        Action action = () => new Moneda(invalidMoneda!);

        // Then
        action.Should().Throw<ArgumentException>();
    }
}
