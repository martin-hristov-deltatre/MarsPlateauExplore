using MarsPlateauExplore.Domain;
using MarsPlateauExplore.Infrastructure.Parsers;

namespace MarsPlateauExplore.UnitTests.Infrastructure.Parsers;

public class AreaSizeParserTests
{
    [Test]
    public void TryParse_WhenXIsZero_ReturnsFailedResult()
    {
        // Arrange
        var coordinates = new Coordinates(0, 5);

        // Act
        var result = AreaSizeParser.TryParse(coordinates);

        // Assert
        Assert.True(result.IsFailure);
        Assert.AreEqual("Invalid input. Please enter positive numbers greater than zero.", result.Error);
    }

    [Test]
    public void TryParse_WhenYIsZero_ReturnsFailedResult()
    {
        // Arrange
        var coordinates = new Coordinates(5, 0);

        // Act
        var result = AreaSizeParser.TryParse(coordinates);

        // Assert
        Assert.True(result.IsFailure);
        Assert.AreEqual("Invalid input. Please enter positive numbers greater than zero.", result.Error);
    }

    [Test]
    public void TryParse_WhenBothArePositive_ReturnsSuccessResult()
    {
        // Arrange
        var coordinates = new Coordinates(5, 5);

        // Act
        var result = AreaSizeParser.TryParse(coordinates);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [TestCase(1, 1)]
    [TestCase(10, 10)]
    [TestCase(100, 100)]
    public void TryParse_WhenBothArePositive_ReturnsSuccessResult(int x, int y)
    {
        // Arrange
        var coordinates = new Coordinates(x, y);

        // Act
        var result = AreaSizeParser.TryParse(coordinates);

        // Assert
        Assert.True(result.IsSuccess);
    }
}
