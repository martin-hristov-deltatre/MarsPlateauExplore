using MarsPlateauExplore.Domain;
using MarsPlateauExplore.Infrastructure;
using MarsPlateauExplore.Infrastructure.Parsers;

namespace MarsPlateauExplore.UnitTests.Infrastructure.Parsers;
public class CoordinateParserTests
{
    [Test]
    public void TryParse_ValidInput_ReturnsSuccess()
    {
        var result = CoordinateParser.TryParse("3 5", out Coordinates coordinates);

        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(new Coordinates(3, 5), coordinates);
    }

    [Test]
    public void TryParse_ValidInput_ZeroCoordinates_ReturnsSuccess()
    {
        var result = CoordinateParser.TryParse("0 0", out Coordinates coordinates);

        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(new Coordinates(0, 0), coordinates);
    }

    [Test]
    public void TryParse_NullInput_ReturnsFailure()
    {
        var result = CoordinateParser.TryParse(null, out Coordinates coordinates);

        Assert.IsTrue(result.IsFailure);
        Assert.AreEqual("Input cannot be null. Please enter two coordinates.", result.Error);
    }

    [Test]
    public void TryParse_EmptyInput_ReturnsFailure()
    {
        var result = CoordinateParser.TryParse("", out Coordinates coordinates);

        Assert.IsTrue(result.IsFailure);
        Assert.AreEqual("Invalid input. Please enter two coordinates.", result.Error);
    }

    [Test]
    public void TryParse_SingleCoordinate_ReturnsFailure()
    {
        var result = CoordinateParser.TryParse("3", out Coordinates coordinates);

        Assert.IsTrue(result.IsFailure);
        Assert.AreEqual("Invalid input. Please enter two coordinates.", result.Error);
    }

    [Test]
    public void TryParse_FirstCoordinateNonNumeric_ReturnsFailure()
    {
        var result = CoordinateParser.TryParse("abc 5", out Coordinates coordinates);

        Assert.IsTrue(result.IsFailure);
        Assert.AreEqual("Invalid input. Please enter valid positive numbers.", result.Error);
    }

    [Test]
    public void TryParse_SecondCoordinateNonNumeric_ReturnsFailure()
    {
        var result = CoordinateParser.TryParse("3 abc", out Coordinates coordinates);

        Assert.IsTrue(result.IsFailure);
        Assert.AreEqual("Invalid input. Please enter valid positive numbers.", result.Error);
    }

    [Test]
    public void TryParse_NegativeX_ReturnsFailure()
    {
        var result = CoordinateParser.TryParse("-1 5", out Coordinates coordinates);

        Assert.IsTrue(result.IsFailure);
        Assert.AreEqual("Invalid input. Please enter valid positive numbers.", result.Error);
    }

    [Test]
    public void TryParse_NegativeY_ReturnsFailure()
    {
        var result = CoordinateParser.TryParse("3 -1", out Coordinates coordinates);

        Assert.IsTrue(result.IsFailure);
        Assert.AreEqual("Invalid input. Please enter valid positive numbers.", result.Error);
    }

    [Test]
    public void TryParse_InvalidInput_CoordinatesDefaultToZero()
    {
        var result = CoordinateParser.TryParse("abc def", out Coordinates coordinates);

        Assert.IsTrue(result.IsFailure);
        Assert.AreEqual(new Coordinates(0, 0), coordinates);
    }
}
