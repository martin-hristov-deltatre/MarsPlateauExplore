using MarsPlateauExplore.Domain;
using MarsPlateauExplore.Enums;
using MarsPlateauExplore.Parsers;

namespace MarsPlateauExplore;

public class InputReceiver
{
    public Coordinates GetAreaSize()
    {
        Console.WriteLine("Enter max coordinates of the plateau like 5 5");

        while (true)
        {
            var input = Console.ReadLine();
            var (isSuccess, error) = TryParseAreaSize(input!, out Coordinates coordinates);
            if (!isSuccess)
            {
                Console.WriteLine(error);
                continue;
            }

            return coordinates;
        }
    }

    internal static (bool isSuccess, string error) TryParseAreaSize(string input, out Coordinates coordinates)
    {
        var (isCoordinateParseSuccess, coordinateParseError) = CoordinateParser.TryParse(input, out coordinates);
        if (!isCoordinateParseSuccess)
        {
            return (false, coordinateParseError);
        }

        var (areaSizeParseSuccess, areaSizeError) = AreaSizeParser.TryParse(coordinates);
        if (!areaSizeParseSuccess)
        {
            return (false, areaSizeError);
        }

        Console.WriteLine($"Plateau size is: {coordinates.X} {coordinates.Y}");

        return (true, string.Empty);
    }

    public (Coordinates Coordinates, Direction Direction) GetRoverCoordinatesAndDirection(int roverId)
    {
        Console.WriteLine($"Enter the initial position of the rover {roverId} like 1 2 N");

        while (true)
        {
            var input = Console.ReadLine();

            var (isSuccess, error) = TryParseRoverCoordinatesAndDirection(input!, out Coordinates coordinates, out Direction direction);
            if (!isSuccess)
            {
                Console.WriteLine(error);
                continue;
            }

            Console.WriteLine($"Rover initial position is: {coordinates.X} {coordinates.Y} {direction}");
            return (coordinates, direction);
        }
    }

    internal static (bool isSuccess, string error) TryParseRoverCoordinatesAndDirection(
        string input, out Coordinates coordinates, out Direction direction)
    {
        var (isCoordinateParseSuccess, coordinateParseError) = CoordinateParser.TryParse(input, out coordinates);
        if (!isCoordinateParseSuccess)
        {
            direction = default;
            return (false, coordinateParseError);
        }

        var (isDirectionParseSuccess, directionParseError) = DirectionParser.TryParse(input, out direction);
        if (!isDirectionParseSuccess)
        {
            return (false, directionParseError);
        }

        return (true, string.Empty);
    }

    public List<Instruction> GetRoverInstructions(int roverId)
    {
        Console.WriteLine($"Enter the instructions for rover {roverId}");

        while (true)
        {
            var input = Console.ReadLine();

            var (isSuccess, error) = TryParseRoverInstructions(input!, out List<Instruction> instructions);
            if (!isSuccess)
            {
                Console.WriteLine(error);
                continue;
            }

            return instructions;
        }
    }

    internal static (bool isSuccess, string error) TryParseRoverInstructions(string input, out List<Instruction> instructions)
    {
        var (isInstructionsParseSuccess, instructionsParseError) = InstructionsParser.TryParse(input, out instructions);
        if (!isInstructionsParseSuccess)
        {
            return (false, instructionsParseError);
        }
        return (true, string.Empty);
    }
}