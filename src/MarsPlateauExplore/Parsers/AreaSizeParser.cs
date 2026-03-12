using MarsPlateauExplore.Domain;

namespace MarsPlateauExplore.Parsers;

public static class AreaSizeParser
{
    public static (bool isSuccess, string error) TryParse(Coordinates coordinates)
    {
        if (coordinates.X == 0 || coordinates.Y == 0)
            return (false, "Invalid input. Please enter positive numbers greater than zero.");

        // can be check there's enough space for the rovers to move around, but it is not specified in the requirements, so I will not implement it

        return (true, string.Empty);
    }
}