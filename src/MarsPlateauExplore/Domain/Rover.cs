using MarsPlateauExplore.Enums;

namespace MarsPlateauExplore.Domain;

public class Rover(int id, Coordinates coordinates, Direction direction)
{
    public int Id { get; set; } = id;
    public Coordinates Coordinates { get; set; } = coordinates;
    public Direction Direction { get; set; } = direction;

    public void TurnLeft()
    {
        Direction[] directions = [Direction.North, Direction.West, Direction.South, Direction.East];
        int currentIndex = Array.IndexOf(directions, Direction);
        Direction = directions[(currentIndex + 1) % directions.Length];
    }

    public void TurnRight()
    {
        Direction[] directions = [Direction.North, Direction.East, Direction.South, Direction.West];
        int currentIndex = Array.IndexOf(directions, Direction);
        Direction = directions[(currentIndex + 1) % directions.Length];
    }

    public Coordinates GetNextCoordinates() => Direction switch
    {
        Direction.North => new Coordinates(Coordinates.X, Coordinates.Y + 1),
        Direction.East  => new Coordinates(Coordinates.X + 1, Coordinates.Y),
        Direction.South => new Coordinates(Coordinates.X, Coordinates.Y - 1),
        Direction.West  => new Coordinates(Coordinates.X - 1, Coordinates.Y),
        _               => new Coordinates(0, 0)
    };

    public void MoveTo()
    {
        Coordinates = GetNextCoordinates();
    }
}
