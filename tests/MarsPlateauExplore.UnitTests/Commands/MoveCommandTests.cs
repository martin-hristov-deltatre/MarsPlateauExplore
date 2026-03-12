using MarsPlateauExplore.Commands;
using MarsPlateauExplore.Domain;
using MarsPlateauExplore.Enums;

namespace MarsPlateauExplore.UnitTests.Commands;
public class MoveCommandTests
{
    private MoveCommand _sut;

    [SetUp]
    public void SetUp()
    {
        _sut = new MoveCommand();
    }

    [Test]
    public void Execute_WhenMoveIsValid_ReturnsSuccess()
    {
        // Arrange
        var area = new Area(new Coordinates(5, 5));
        var rover = new Rover(1, new Coordinates(1, 1), Direction.North);
        var context = new RoverContext(rover, area);

        // Act
        var result = _sut.Execute(context);

        // Assert
        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public void Execute_WhenMoveIsValid_UpdatesRoverCoordinates()
    {
        // Arrange
        var area = new Area(new Coordinates(5, 5));
        var rover = new Rover(1, new Coordinates(1, 1), Direction.North);
        var context = new RoverContext(rover, area);

        // Act
        _sut.Execute(context);

        // Assert
        Assert.That(rover.Coordinates, Is.EqualTo(new Coordinates(1, 2)));
    }

    [Test]
    public void Execute_WhenMoveIsOutOfBounds_ReturnsFailure()
    {
        // Arrange
        var area = new Area(new Coordinates(5, 5));
        var rover = new Rover(1, new Coordinates(0, 5), Direction.North);
        var context = new RoverContext(rover, area);

        // Act
        var result = _sut.Execute(context);

        // Assert
        Assert.That(result.IsSuccess, Is.False);
    }

    [Test]
    public void Execute_WhenMoveIsOutOfBounds_ReturnsErrorMessage()
    {
        // Arrange
        var area = new Area(new Coordinates(5, 5));
        var rover = new Rover(1, new Coordinates(0, 5), Direction.North);
        var context = new RoverContext(rover, area);

        // Act
        var result = _sut.Execute(context);

        // Assert
        Assert.That(result.Error, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void Execute_WhenMoveIsOutOfBounds_DoesNotUpdateRoverCoordinates()
    {
        // Arrange
        var area = new Area(new Coordinates(5, 5));
        var initialCoordinates = new Coordinates(0, 5);
        var rover = new Rover(1, initialCoordinates, Direction.North);
        var context = new RoverContext(rover, area);

        // Act
        _sut.Execute(context);

        // Assert
        Assert.That(rover.Coordinates, Is.EqualTo(initialCoordinates));
    }

    [Test]
    public void Execute_WhenTargetIsOccupied_ReturnsFailure()
    {
        // Arrange
        var area = new Area(new Coordinates(5, 5));
        var rover1 = new Rover(1, new Coordinates(1, 2), Direction.North);
        var rover2 = new Rover(2, new Coordinates(1, 1), Direction.North);

        area.UpsertOccupiedCoordinates(rover1.Id, rover1.Coordinates);

        var context = new RoverContext(rover2, area);

        // Act
        var result = _sut.Execute(context);

        // Assert
        Assert.That(result.IsSuccess, Is.False);
    }

    [Test]
    public void Execute_WhenTargetIsOccupied_ReturnsErrorMessage()
    {
        // Arrange
        var area = new Area(new Coordinates(5, 5));
        var rover1 = new Rover(1, new Coordinates(1, 2), Direction.North);
        var rover2 = new Rover(2, new Coordinates(1, 1), Direction.North);

        area.UpsertOccupiedCoordinates(rover1.Id, rover1.Coordinates);

        var context = new RoverContext(rover2, area);

        // Act
        var result = _sut.Execute(context);

        // Assert
        Assert.That(result.Error, Is.Not.Null.And.Not.Empty);
    }
}
