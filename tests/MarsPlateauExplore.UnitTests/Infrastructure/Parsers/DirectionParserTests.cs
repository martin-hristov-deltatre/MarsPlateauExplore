using MarsPlateauExplore.Enums;
using MarsPlateauExplore.Infrastructure.Parsers;

namespace MarsPlateauExplore.UnitTests.Infrastructure.Parsers;
public class DirectionParserTests
{
    [Test]
    public void TryParse_WithNullInput_ReturnsFailedResult()
    {
        var result = DirectionParser.TryParse(null, out _);

        Assert.That(result.IsFailure, Is.True);
        Assert.That(result.Error, Is.EqualTo("Invalid input. Please enter a direction."));
    }

    [Test]
    public void TryParse_WithEmptyInput_ReturnsFailedResult()
    {
        var result = DirectionParser.TryParse(string.Empty, out _);

        Assert.That(result.IsFailure, Is.True);
        Assert.That(result.Error, Is.EqualTo("Invalid input. Please enter a direction."));
    }

    [Test]
    public void TryParse_WithInsufficientParts_ReturnsFailedResult()
    {
        var result = DirectionParser.TryParse("1 2", out _);

        Assert.That(result.IsFailure, Is.True);
        Assert.That(result.Error, Is.EqualTo("Invalid input. Please enter a direction."));
    }

    [Test]
    public void TryParse_WithTooManyParts_ReturnsFailedResult()
    {
        var result = DirectionParser.TryParse("1 2 N E", out _);

        Assert.That(result.IsFailure, Is.True);
        Assert.That(result.Error, Is.EqualTo("Invalid input. Please enter a direction."));
    }

    [Test]
    public void TryParse_WithInvalidDirection_ReturnsFailedResult()
    {
        var result = DirectionParser.TryParse("1 2 X", out _);

        Assert.That(result.IsFailure, Is.True);
        Assert.That(result.Error, Is.EqualTo("Invalid input. Please enter a valid direction (N, E, S, W)."));
    }

    [TestCase("1 2 N", Direction.North)]
    [TestCase("1 2 E", Direction.East)]
    [TestCase("1 2 S", Direction.South)]
    [TestCase("1 2 W", Direction.West)]
    public void TryParse_WithValidInput_ReturnsSuccessAndCorrectDirection(string input, Direction expectedDirection)
    {
        var result = DirectionParser.TryParse(input, out var direction);

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(direction, Is.EqualTo(expectedDirection));
    }

    [Test]
    public void TryParse_WithValidInput_DefaultDirectionIsNorth()
    {
        DirectionParser.TryParse(null, out var direction);

        Assert.That(direction, Is.EqualTo(Direction.North));
    }

    [Test]
    public void TryParse_WithLowercaseDirection_ReturnsFailedResult()
    {
        var result = DirectionParser.TryParse("1 2 n", out _);

        Assert.That(result.IsFailure, Is.True);
        Assert.That(result.Error, Is.EqualTo("Invalid input. Please enter a valid direction (N, E, S, W)."));
    }
}
