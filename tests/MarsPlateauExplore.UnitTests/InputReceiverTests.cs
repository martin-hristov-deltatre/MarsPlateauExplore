using MarsPlateauExplore.Domain;
using MarsPlateauExplore.Enums;

namespace MarsPlateauExplore.UnitTests;
public class InputReceiverTests
{
    #region TryParseAreaSize Tests

    [Test]
    public void TryParseAreaSize_ValidInput_ReturnsSuccess()
    {
        var result = InputReceiver.TryParseAreaSize("5 5", out Coordinates coordinates);

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(coordinates.X, Is.EqualTo(5));
        Assert.That(coordinates.Y, Is.EqualTo(5));
    }

    [Test]
    public void TryParseAreaSize_InvalidCoordinates_ReturnsFailure()
    {
        var result = InputReceiver.TryParseAreaSize("abc", out Coordinates coordinates);

        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.Error, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void TryParseAreaSize_NegativeCoordinates_ReturnsFailure()
    {
        var result = InputReceiver.TryParseAreaSize("-1 -1", out Coordinates coordinates);

        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.Error, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void TryParseAreaSize_ZeroCoordinates_ReturnsFailure()
    {
        var result = InputReceiver.TryParseAreaSize("0 0", out Coordinates coordinates);

        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.Error, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void TryParseAreaSize_EmptyInput_ReturnsFailure()
    {
        var result = InputReceiver.TryParseAreaSize(string.Empty, out Coordinates coordinates);

        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.Error, Is.Not.Null.And.Not.Empty);
    }
    #endregion

    #region TryParseRoverCoordinatesAndDirection Tests

    [Test]
    public void TryParseRoverCoordinatesAndDirection_ValidInput_ReturnsSuccess()
    {
        var result = InputReceiver.TryParseRoverCoordinatesAndDirection(
            "1 2 N", out Coordinates coordinates, out Direction direction);

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(coordinates.X, Is.EqualTo(1));
        Assert.That(coordinates.Y, Is.EqualTo(2));
        Assert.That(direction, Is.EqualTo(Direction.North));
    }

    [Test]
    [TestCase("1 2 E", 1, 2, Direction.East)]
    [TestCase("3 4 S", 3, 4, Direction.South)]
    [TestCase("0 0 W", 0, 0, Direction.West)]
    public void TryParseRoverCoordinatesAndDirection_ValidDirections_ReturnsCorrectDirection(
        string input, int expectedX, int expectedY, Direction expectedDirection)
    {
        var result = InputReceiver.TryParseRoverCoordinatesAndDirection(
            input, out Coordinates coordinates, out Direction direction);

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(coordinates.X, Is.EqualTo(expectedX));
        Assert.That(coordinates.Y, Is.EqualTo(expectedY));
        Assert.That(direction, Is.EqualTo(expectedDirection));
    }

    [Test]
    public void TryParseRoverCoordinatesAndDirection_InvalidCoordinates_ReturnsFailure()
    {
        var result = InputReceiver.TryParseRoverCoordinatesAndDirection(
            "abc N", out Coordinates coordinates, out Direction direction);

        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.Error, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void TryParseRoverCoordinatesAndDirection_InvalidDirection_ReturnsFailure()
    {
        var result = InputReceiver.TryParseRoverCoordinatesAndDirection(
            "1 2 X", out Coordinates coordinates, out Direction direction);

        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.Error, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void TryParseRoverCoordinatesAndDirection_EmptyInput_ReturnsFailure()
    {
        var result = InputReceiver.TryParseRoverCoordinatesAndDirection(
            string.Empty, out Coordinates coordinates, out Direction direction);

        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.Error, Is.Not.Null.And.Not.Empty);
    }
    #endregion

    #region TryParseRoverInstructions Tests

    [Test]
    public void TryParseRoverInstructions_ValidInput_ReturnsSuccess()
    {
        var result = InputReceiver.TryParseRoverInstructions("LRMLRM", out List<Instruction> instructions);

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(instructions, Is.Not.Null);
        Assert.That(instructions, Has.Count.EqualTo(6));
    }

    [Test]
    public void TryParseRoverInstructions_ValidInput_ReturnCorrectInstructions()
    {
        var result = InputReceiver.TryParseRoverInstructions("LRM", out List<Instruction> instructions);

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(instructions[0], Is.EqualTo(Instruction.TurnLeft));
        Assert.That(instructions[1], Is.EqualTo(Instruction.TurnRight));
        Assert.That(instructions[2], Is.EqualTo(Instruction.MoveForward));
    }

    [Test]
    public void TryParseRoverInstructions_InvalidCharacters_ReturnsFailure()
    {
        var result = InputReceiver.TryParseRoverInstructions("LXR", out List<Instruction> instructions);

        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.Error, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void TryParseRoverInstructions_EmptyInput_ReturnsFailure()
    {
        var result = InputReceiver.TryParseRoverInstructions(string.Empty, out List<Instruction> instructions);

        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.Error, Is.Not.Null.And.Not.Empty);
    }

    #endregion
}
