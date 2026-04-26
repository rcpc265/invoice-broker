using FluentAssertions;
using InvoiceBroker.Domain.ValueObjects;

namespace InvoiceBroker.UnitTests.Domain.ValueObjects;

public class MontoTests
{
    [Fact]
    public void Given_ValidValue_When_Created_Then_ValueIsSet()
    {
        // Given
        decimal validValue = 100.50m;

        // When
        Monto monto = new Monto(validValue);

        // Then
        monto.Value.Should().Be(100.50m);
    }

    [Fact]
    public void Given_NegativeValue_When_Created_Then_ThrowsArgumentException()
    {
        // Given
        decimal negativeValue = -10m;

        // When
        Action action = () => new Monto(negativeValue);

        // Then
        action.Should().Throw<ArgumentException>();
    }
}
