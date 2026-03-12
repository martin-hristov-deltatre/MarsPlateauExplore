using MarsPlateauExplore.Domain;
using MarsPlateauExplore.Enums;

namespace MarsPlateauExplore.UnitTests.Domain;
public class RoverTests
{
    #region TurnLeft Tests
    [Test]
    public void TurnLeft_WhenFacingNorth_ShouldFaceWest()
    {
        var rover = new Rover(1, new Coordinates(0, 0), Direction.North);
        rover.TurnLeft();
        Assert.That(rover.Direction, Is.EqualTo(Direction.West));
    }

    [Test]
    public void TurnLeft_WhenFacingWest_ShouldFaceSouth()
    {
        var rover = new Rover(1, new Coordinates(0, 0), Direction.West);
        rover.TurnLeft();
        Assert.That(rover.Direction, Is.EqualTo(Direction.South));
    }

    [Test]
    public void TurnLeft_WhenFacingSouth_ShouldFaceEast()
    {
        var rover = new Rover(1, new Coordinates(0, 0), Direction.South);
        rover.TurnLeft();
        Assert.That(rover.Direction, Is.EqualTo(Direction.East));
    }

    [Test]
    public void TurnLeft_WhenFacingEast_ShouldFaceNorth()
    {
        var rover = new Rover(1, new Coordinates(0, 0), Direction.East);
        rover.TurnLeft();
        Assert.That(rover.Direction, Is.EqualTo(Direction.North));
    }
    #endregion

    #region TurnRight Tests
    [Test]
    public void TurnRight_WhenFacingNorth_ShouldFaceEast()
    {
        var rover = new Rover(1, new Coordinates(0, 0), Direction.North);
        rover.TurnRight();
        Assert.That(rover.Direction, Is.EqualTo(Direction.East));
    }

    [Test]
    public void TurnRight_WhenFacingEast_ShouldFaceSouth()
    {
        var rover = new Rover(1, new Coordinates(0, 0), Direction.East);
        rover.TurnRight();
        Assert.That(rover.Direction, Is.EqualTo(Direction.South));
    }

    [Test]
    public void TurnRight_WhenFacingSouth_ShouldFaceWest()
    {
        var rover = new Rover(1, new Coordinates(0, 0), Direction.South);
        rover.TurnRight();
        Assert.That(rover.Direction, Is.EqualTo(Direction.West));
    }

    [Test]
    public void TurnRight_WhenFacingWest_ShouldFaceNorth()
    {
        var rover = new Rover(1, new Coordinates(0, 0), Direction.West);
        rover.TurnRight();
        Assert.That(rover.Direction, Is.EqualTo(Direction.North));
    }
    #endregion

    #region GetNextCoordinates Tests
    [Test]
    public void GetNextCoordinates_WhenFacingNorth_ShouldIncrementY()
    {
        var rover = new Rover(1, new Coordinates(2, 3), Direction.North);
        var next = rover.GetNextCoordinates();
        Assert.That(next.X, Is.EqualTo(2));
        Assert.That(next.Y, Is.EqualTo(4));
    }

    [Test]
    public void GetNextCoordinates_WhenFacingEast_ShouldIncrementX()
    {
        var rover = new Rover(1, new Coordinates(2, 3), Direction.East);
        var next = rover.GetNextCoordinates();
        Assert.That(next.X, Is.EqualTo(3));
        Assert.That(next.Y, Is.EqualTo(3));
    }

    [Test]
    public void GetNextCoordinates_WhenFacingSouth_ShouldDecrementY()
    {
        var rover = new Rover(1, new Coordinates(2, 3), Direction.South);
        var next = rover.GetNextCoordinates();
        Assert.That(next.X, Is.EqualTo(2));
        Assert.That(next.Y, Is.EqualTo(2));
    }

    [Test]
    public void GetNextCoordinates_WhenFacingWest_ShouldDecrementX()
    {
        var rover = new Rover(1, new Coordinates(2, 3), Direction.West);
        var next = rover.GetNextCoordinates();
        Assert.That(next.X, Is.EqualTo(1));
        Assert.That(next.Y, Is.EqualTo(3));
    }

    // MoveTo Tests
    [Test]
    public void MoveTo_WhenFacingNorth_ShouldUpdateCoordinates()
    {
        var rover = new Rover(1, new Coordinates(2, 3), Direction.North);
        rover.MoveTo();
        Assert.That(rover.Coordinates.X, Is.EqualTo(2));
        Assert.That(rover.Coordinates.Y, Is.EqualTo(4));
    }

    [Test]
    public void MoveTo_WhenFacingEast_ShouldUpdateCoordinates()
    {
        var rover = new Rover(1, new Coordinates(2, 3), Direction.East);
        rover.MoveTo();
        Assert.That(rover.Coordinates.X, Is.EqualTo(3));
        Assert.That(rover.Coordinates.Y, Is.EqualTo(3));
    }

    [Test]
    public void MoveTo_WhenFacingSouth_ShouldUpdateCoordinates()
    {
        var rover = new Rover(1, new Coordinates(2, 3), Direction.South);
        rover.MoveTo();
        Assert.That(rover.Coordinates.X, Is.EqualTo(2));
        Assert.That(rover.Coordinates.Y, Is.EqualTo(2));
    }

    [Test]
    public void MoveTo_WhenFacingWest_ShouldUpdateCoordinates()
    {
        var rover = new Rover(1, new Coordinates(2, 3), Direction.West);
        rover.MoveTo();
        Assert.That(rover.Coordinates.X, Is.EqualTo(1));
        Assert.That(rover.Coordinates.Y, Is.EqualTo(3));
    }
    #endregion
}
