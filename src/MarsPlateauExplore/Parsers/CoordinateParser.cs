using MarsPlateauExplore.Domain;

namespace MarsPlateauExplore.Parsers;
public static class CoordinateParser
{
    public static (bool isSuccess, string error) TryParse(string? input, out Coordinates coordinates)
    {
        coordinates = new Coordinates(0, 0);

        var parts = input?.Split(' ');

        if (parts?.Length < 2)
            return (false, "Invalid input. Please enter two coordinates.");

        if (!int.TryParse(parts[0], out int x) || !int.TryParse(parts[1], out int y))
            return (false, "Invalid input. Please enter valid positive numbers.");

        if (x < 0 || y < 0)
        {
            return (false, "Invalid input. Please enter valid positive numbers.");
        }

        coordinates = new Coordinates(x, y);
        return (true, string.Empty);
    }
}
