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
}
