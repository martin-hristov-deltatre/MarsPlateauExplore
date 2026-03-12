using MarsPlateauExplore.Domain;
using MarsPlateauExplore.Enums;
using MarsPlateauExplore.Infrastructure;
using MarsPlateauExplore.Infrastructure.Parsers;

namespace MarsPlateauExplore.Application;

public class InputReceiver : IInputReceiver
{
    public Coordinates GetAreaSize()
    {
        Console.WriteLine("Enter max coordinates of the plateau like 5 5");

        while (true)
        {
            var input = Console.ReadLine();
            var result = TryParseAreaSize(input!, out Coordinates coordinates);
            if (!result.IsSuccess)
            {
                Console.WriteLine(result.Error);
                continue;
            }

            return coordinates;
        }
    }

    internal static Result TryParseAreaSize(string input, out Coordinates coordinates)
    {
        var coordinateParseResult = CoordinateParser.TryParse(input, out coordinates);
        if (!coordinateParseResult.IsSuccess)
        {
            return Result.Failed(coordinateParseResult.Error);
        }

        var areaSizeResult = AreaSizeParser.TryParse(coordinates);
        if (!areaSizeResult.IsSuccess)
        {
            return Result.Failed(areaSizeResult.Error);
        }

        Console.WriteLine($"Plateau size is: {coordinates.X} {coordinates.Y}");

        return Result.Success();
    }

    public (Coordinates Coordinates, Direction Direction) GetRoverCoordinatesAndDirection(int roverId)
    {
        Console.WriteLine($"Enter the initial position of the rover {roverId} like 1 2 N");

        while (true)
        {
            var input = Console.ReadLine();

            var result = TryParseRoverCoordinatesAndDirection(input!, out Coordinates coordinates, out Direction direction);
            if (!result.IsSuccess)
            {
                Console.WriteLine(result.Error);
                continue;
            }

            Console.WriteLine($"Rover initial position is: {coordinates.X} {coordinates.Y} {direction}");
            return (coordinates, direction);
        }
    }

    internal static Result TryParseRoverCoordinatesAndDirection(
        string input, out Coordinates coordinates, out Direction direction)
    {
        var coordinateParseResult = CoordinateParser.TryParse(input, out coordinates);
        if (!coordinateParseResult.IsSuccess)
        {
            direction = default;
            return Result.Failed(coordinateParseResult.Error);
        }

        var directionParseResult = DirectionParser.TryParse(input, out direction);
        if (!directionParseResult.IsSuccess)
        {
            return Result.Failed(directionParseResult.Error);
        }

        return Result.Success();
    }

    public List<Instruction> GetRoverInstructions(int roverId)
    {
        Console.WriteLine($"Enter the instructions for rover {roverId}");

        while (true)
        {
            var input = Console.ReadLine();

            var result = TryParseRoverInstructions(input!, out List<Instruction> instructions);
            if (!result.IsSuccess)
            {
                Console.WriteLine(result.Error);
                continue;
            }

            return instructions;
        }
    }

    internal static Result TryParseRoverInstructions(string input, out List<Instruction> instructions)
    {
        var instructionsParseResult = InstructionsParser.TryParse(input, out instructions);
        if (!instructionsParseResult.IsSuccess)
        {
            return Result.Failed(instructionsParseResult.Error);
        }
        return Result.Success();
    }
}