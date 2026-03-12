using MarsPlateauExplore.Domain;

namespace MarsPlateauExplore.Infrastructure.Parsers;

public static class AreaSizeParser
{
    public static Result TryParse(Coordinates coordinates)
    {
        if (coordinates.X == 0 || coordinates.Y == 0)
            return Result.Failed("Invalid input. Please enter positive numbers greater than zero.");

        // can be check there's enough space for the rovers to move around, but it is not specified in the requirements, so I will not implement it

        return Result.Success();
    }
}