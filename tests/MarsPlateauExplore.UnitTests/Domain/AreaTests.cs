using MarsPlateauExplore.Domain;
using MarsPlateauExplore.Enums;

namespace MarsPlateauExplore.UnitTests.Domain;
public class AreaTests
{
    private Area _area;

    [SetUp]
    public void SetUp()
    {
        _area = new Area(new Coordinates(5, 5));
    }

    #region IsWithinBounds

    [Test]
    public void IsWithinBounds_WhenCoordinatesAreWithinBounds_ReturnsTrue()
    {
        var coordinates = new Coordinates(3, 3);

        var result = _area.IsWithinBounds(coordinates);

        Assert.That(result, Is.True);
    }

    [Test]
    public void IsWithinBounds_WhenCoordinatesAreOnMaxEdge_ReturnsTrue()
    {
        var coordinates = new Coordinates(5, 5);

        var result = _area.IsWithinBounds(coordinates);

        Assert.That(result, Is.True);
    }

    [Test]
    public void IsWithinBounds_WhenCoordinatesAreOnOrigin_ReturnsTrue()
    {
        var coordinates = new Coordinates(0, 0);

        var result = _area.IsWithinBounds(coordinates);

        Assert.That(result, Is.True);
    }

    [Test]
    public void IsWithinBounds_WhenXExceedsMaxDimension_ReturnsFalse()
    {
        var coordinates = new Coordinates(6, 3);

        var result = _area.IsWithinBounds(coordinates);

        Assert.That(result, Is.False);
    }

    [Test]
    public void IsWithinBounds_WhenYExceedsMaxDimension_ReturnsFalse()
    {
        var coordinates = new Coordinates(3, 6);

        var result = _area.IsWithinBounds(coordinates);

        Assert.That(result, Is.False);
    }

    [Test]
    public void IsWithinBounds_WhenXIsNegative_ReturnsFalse()
    {
        var coordinates = new Coordinates(-1, 3);

        var result = _area.IsWithinBounds(coordinates);

        Assert.That(result, Is.False);
    }

    [Test]
    public void IsWithinBounds_WhenYIsNegative_ReturnsFalse()
    {
        var coordinates = new Coordinates(3, -1);

        var result = _area.IsWithinBounds(coordinates);

        Assert.That(result, Is.False);
    }

    #endregion

    #region IsOccupied

    [Test]
    public void IsOccupied_WhenNoRoversRegistered_ReturnsFalse()
    {
        var coordinates = new Coordinates(2, 2);

        var result = _area.IsOccupied(1, coordinates);

        Assert.That(result, Is.False);
    }

    [Test]
    public void IsOccupied_WhenAnotherRoverOccupiesCoordinates_ReturnsTrue()
    {
        var coordinates = new Coordinates(2, 2);
        _area.UpsertOccupiedCoordinates(1, coordinates);

        var result = _area.IsOccupied(2, coordinates);

        Assert.That(result, Is.True);
    }

    [Test]
    public void IsOccupied_WhenSameRoverOccupiesCoordinates_ReturnsFalse()
    {
        var coordinates = new Coordinates(2, 2);
        _area.UpsertOccupiedCoordinates(1, coordinates);

        var result = _area.IsOccupied(1, coordinates);

        Assert.That(result, Is.False);
    }

    [Test]
    public void IsOccupied_WhenAnotherRoverOccupiesDifferentCoordinates_ReturnsFalse()
    {
        _area.UpsertOccupiedCoordinates(1, new Coordinates(1, 1));

        var result = _area.IsOccupied(2, new Coordinates(2, 2));

        Assert.That(result, Is.False);
    }

    #endregion

    #region UpsertOccupiedCoordinates

    [Test]
    public void UpsertOccupiedCoordinates_WhenRoverNotRegistered_RegistersRover()
    {
        var coordinates = new Coordinates(2, 2);

        _area.UpsertOccupiedCoordinates(1, coordinates);

        Assert.That(_area.IsOccupied(2, coordinates), Is.True);
    }

    [Test]
    public void UpsertOccupiedCoordinates_WhenRoverAlreadyRegistered_UpdatesCoordinates()
    {
        _area.UpsertOccupiedCoordinates(1, new Coordinates(1, 1));
        var newCoordinates = new Coordinates(3, 3);

        _area.UpsertOccupiedCoordinates(1, newCoordinates);

        Assert.That(_area.IsOccupied(2, newCoordinates), Is.True);
        Assert.That(_area.IsOccupied(2, new Coordinates(1, 1)), Is.False);
    }

    #endregion

    #region TryMove

    [Test]
    public void TryMove_WhenMoveIsValid_ReturnsSuccess()
    {
        var rover = new Rover(1, new Coordinates(0, 0), Direction.North);
        var targetCoordinates = new Coordinates(1, 1);

        var result = _area.TryMove(rover, targetCoordinates);

        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public void TryMove_WhenMoveIsValid_UpdatesOccupiedCoordinates()
    {
        var rover = new Rover(1, new Coordinates(0, 0), Direction.North);
        var targetCoordinates = new Coordinates(1, 1);

        _area.TryMove(rover, targetCoordinates);

        Assert.That(_area.IsOccupied(2, targetCoordinates), Is.True);
    }

    [Test]
    public void TryMove_WhenTargetCoordinatesAreOutOfBounds_ReturnsFailure()
    {
        var rover = new Rover(1, new Coordinates(0, 0), Direction.North);
        var targetCoordinates = new Coordinates(6, 6);

        var result = _area.TryMove(rover, targetCoordinates);

        Assert.That(result.IsFailure, Is.True);
    }

    [Test]
    public void TryMove_WhenTargetCoordinatesAreOutOfBounds_ReturnsExpectedErrorMessage()
    {
        var rover = new Rover(1, new Coordinates(0, 0), Direction.North);
        var targetCoordinates = new Coordinates(6, 6);

        var result = _area.TryMove(rover, targetCoordinates);

        Assert.That(result.Error, Is.EqualTo($"Rover {rover.Id} cannot move to ({targetCoordinates.X}, {targetCoordinates.Y}) as it is out of bounds."));
    }

    [Test]
    public void TryMove_WhenTargetCoordinatesAreOccupiedByAnotherRover_ReturnsFailure()
    {
        var rover1 = new Rover(1, new Coordinates(0, 0), Direction.North);
        var rover2 = new Rover(2, new Coordinates(1, 1), Direction.North);
        var targetCoordinates = new Coordinates(1, 1);
        _area.UpsertOccupiedCoordinates(rover2.Id, targetCoordinates);

        var result = _area.TryMove(rover1, targetCoordinates);

        Assert.That(result.IsFailure, Is.True);
    }

    [Test]
    public void TryMove_WhenTargetCoordinatesAreOccupiedByAnotherRover_ReturnsExpectedErrorMessage()
    {
        var rover1 = new Rover(1, new Coordinates(0, 0), Direction.North);
        var rover2 = new Rover(2, new Coordinates(1, 1), Direction.North);
        var targetCoordinates = new Coordinates(1, 1);
        _area.UpsertOccupiedCoordinates(rover2.Id, targetCoordinates);

        var result = _area.TryMove(rover1, targetCoordinates);

        Assert.That(result.Error, Is.EqualTo($"Rover {rover1.Id} cannot move to ({targetCoordinates.X}, {targetCoordinates.Y}) as it is occupied by another rover."));
    }

    #endregion
}
