using MarsPlateauExplore.Enums;

namespace MarsPlateauExplore.Parsers;

public static class DirectionParser
{
    private static readonly Dictionary<string, Direction> _directionMap = new Dictionary<string, Direction>
    {
        { "N", Direction.North },
        { "E", Direction.East },
        { "S", Direction.South },
        { "W", Direction.West }
    };

    public static (bool isSuccess, string error) TryParse(string? input, out Direction direction)
    {
        direction = Direction.North;

        var positionsAndDirection = input?.Split(' ');
        if (positionsAndDirection?.Length != 3)
        {
            return (false, "Invalid input. Please enter a direction.");
        }

        var directionInput = positionsAndDirection[2];

        if (!_directionMap.TryGetValue(directionInput, out direction))
        {
            return (false, "Invalid input. Please enter a valid direction (N, E, S, W).");
        }

        return (true, string.Empty);
    }
}
