using MarsPlateauExplore.Infrastructure;

namespace MarsPlateauExplore.Domain;

public class Area(Coordinates maxDimension)
{
    private readonly Dictionary<int, Coordinates> _occupiedCoordinates = [];

    Coordinates MaxDimension { get; set; } = maxDimension;

    public bool IsWithinBounds(Coordinates targetCoordinates) =>
        targetCoordinates.X >= 0 && targetCoordinates.X <= MaxDimension.X &&
        targetCoordinates.Y >= 0 && targetCoordinates.Y <= MaxDimension.Y;

    public bool IsOccupied(int id, Coordinates coordinates) =>
        _occupiedCoordinates.Any(_occupiedCoordinates => _occupiedCoordinates.Key != id && _occupiedCoordinates.Value == coordinates);

    public void UpsertOccupiedCoordinates(int id, Coordinates coordinates)
    {
        _occupiedCoordinates[id] = coordinates;
    }

    public Result TryMove(Rover rover, Coordinates coordinates)
    {
        if (!IsWithinBounds(coordinates))
            return Result.Failed($"Rover {rover.Id} cannot move to ({coordinates.X}, {coordinates.Y}) as it is out of bounds.");

        if (IsOccupied(rover.Id, coordinates))
            return Result.Failed($"Rover {rover.Id} cannot move to ({coordinates.X}, {coordinates.Y}) as it is occupied by another rover.");

        UpsertOccupiedCoordinates(rover.Id, coordinates);

        return Result.Success();
    }
}
