using MarsPlateauExplore.Domain;

namespace MarsPlateauExplore.Infrastructure.Parsers;
public static class CoordinateParser
{
    public static Result TryParse(string? input, out Coordinates coordinates)
    {
        coordinates = new Coordinates(0, 0);

        if (input == null)
            return Result.Failed("Input cannot be null. Please enter two coordinates.");

        var parts = input?.Split(' ');

        if (parts?.Length < 2)
            return Result.Failed("Invalid input. Please enter two coordinates.");

        if (!int.TryParse(parts[0], out int x) || !int.TryParse(parts[1], out int y))
            return Result.Failed("Invalid input. Please enter valid positive numbers.");

        if (x < 0 || y < 0)
        {
            return Result.Failed("Invalid input. Please enter valid positive numbers.");
        }

        coordinates = new Coordinates(x, y);
        return Result.Success();
    }
}
