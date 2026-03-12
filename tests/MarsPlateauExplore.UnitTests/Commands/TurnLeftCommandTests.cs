using MarsPlateauExplore.Commands;
using MarsPlateauExplore.Domain;
using MarsPlateauExplore.Enums;

namespace MarsPlateauExplore.UnitTests.Commands;
public class TurnLeftCommandTests
{
    private TurnLeftCommand _command;

    [SetUp]
    public void SetUp()
    {
        _command = new TurnLeftCommand();
    }

    [Test]
    public void Execute_ShouldReturnSuccess()
    {
        // Arrange
        var rover = new Rover(1, new Coordinates(0, 0), Direction.North);
        var area = new Area(new Coordinates(5, 5));
        var context = new RoverContext(rover, area);

        // Act
        var result = _command.Execute(context);

        // Assert
        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public void Execute_WhenFacingNorth_ShouldFaceWest()
    {
        // Arrange
        var rover = new Rover(1, new Coordinates(0, 0), Direction.North);
        var area = new Area(new Coordinates(5, 5));
        var context = new RoverContext(rover, area);

        // Act
        _command.Execute(context);

        // Assert
        Assert.That(rover.Direction, Is.EqualTo(Direction.West));
    }

    [Test]
    public void Execute_WhenFacingWest_ShouldFaceSouth()
    {
        // Arrange
        var rover = new Rover(1, new Coordinates(0, 0), Direction.West);
        var area = new Area(new Coordinates(5, 5));
        var context = new RoverContext(rover, area);

        // Act
        _command.Execute(context);

        // Assert
        Assert.That(rover.Direction, Is.EqualTo(Direction.South));
    }

    [Test]
    public void Execute_WhenFacingSouth_ShouldFaceEast()
    {
        // Arrange
        var rover = new Rover(1, new Coordinates(0, 0), Direction.South);
        var area = new Area(new Coordinates(5, 5));
        var context = new RoverContext(rover, area);

        // Act
        _command.Execute(context);

        // Assert
        Assert.That(rover.Direction, Is.EqualTo(Direction.East));
    }

    [Test]
    public void Execute_WhenFacingEast_ShouldFaceNorth()
    {
        // Arrange
        var rover = new Rover(1, new Coordinates(0, 0), Direction.East);
        var area = new Area(new Coordinates(5, 5));
        var context = new RoverContext(rover, area);

        // Act
        _command.Execute(context);

        // Assert
        Assert.That(rover.Direction, Is.EqualTo(Direction.North));
    }

    [Test]
    public void Execute_ShouldNotChangeRoverCoordinates()
    {
        // Arrange
        var initialCoordinates = new Coordinates(2, 3);
        var rover = new Rover(1, initialCoordinates, Direction.North);
        var area = new Area(new Coordinates(5, 5));
        var context = new RoverContext(rover, area);

        // Act
        _command.Execute(context);

        // Assert
        Assert.That(rover.Coordinates, Is.EqualTo(initialCoordinates));
    }
}
