using MarsPlateauExplore.Application;
using MarsPlateauExplore.Domain;
using MarsPlateauExplore.Enums;
using MarsPlateauExplore.Extensions;
using MarsPlateauExplore.Infrastructure;

namespace MarsPlateauExplore;

public class PlateauExplorer
{
    private const int RoverCount = 2;
    private int _roverId = 1;
    private readonly Dictionary<int, Rover> _rovers = [];
    private readonly IInputReceiver _inputReceiver;
    private readonly Area _area;
    private readonly IRoverStatusRegistry _roverStatusRegistry;

    public PlateauExplorer(IInputReceiver inputReceiver, Area area, IRoverStatusRegistry roverStatusRegistry)
    {
        _inputReceiver = inputReceiver;
        _area = area;
        _roverStatusRegistry = roverStatusRegistry;
    }

    public void Execute(IRoverController roverController)
    {
        while (true)
        {
            ExecuteOnce(roverController);
        }
    }

    internal Result ExecuteOnce(IRoverController roverController)
    {
        var roverCoordinatesAndDirection = _inputReceiver.GetRoverCoordinatesAndDirection(_roverId);
        var instructions = _inputReceiver.GetRoverInstructions(_roverId);

        var rover = GetOrCreateRover(_roverId, roverCoordinatesAndDirection);

        var result = roverController.Execute(rover, instructions, _area, _roverStatusRegistry);
        if (!result.IsSuccess)
        {
            Console.WriteLine($"Error: {result.Error}");
            AdvanceRoverId();
            Console.WriteLine($"{rover.Coordinates.X} {rover.Coordinates.Y} {rover.Direction.GetDescription()}");
            return Result.Failed(result.Error);
        }

        Console.WriteLine($"Rover {rover.Id} moved successfully.");
        Console.WriteLine($"{rover.Coordinates.X} {rover.Coordinates.Y} {rover.Direction.GetDescription()}");

        AdvanceRoverId();
        return Result.Success();
    }

    private Rover GetOrCreateRover(int id, (Coordinates Coordinates, Direction Direction) positionData)
    {
        if (!_rovers.TryGetValue(id, out var rover))
        {
            rover = new Rover(id, positionData.Coordinates, positionData.Direction);
            _rovers[id] = rover;
            Console.WriteLine($"Rover {rover.Id} created at {rover.Coordinates.X} {rover.Coordinates.Y} facing {rover.Direction.GetDescription()}.");
        }
        return rover;
    }

    private void AdvanceRoverId()
    {
        _roverId = _roverId == RoverCount ? 1 : _roverId + 1;
    }
}
