using MarsPlateauExplore.Controllers;
using MarsPlateauExplore.Domain;
using MarsPlateauExplore.Enums;
using MarsPlateauExplore.Extensions;

namespace MarsPlateauExplore;

public class PlateauExplorer
{
    private const int RoverCount = 2;
    private int _roverId = 1;
    private readonly Dictionary<int, Rover> _rovers = [];
    private readonly InputReceiver _inputReceiver;
    private readonly Area _area;
    private readonly RoverStatusRegistry _roverStatusRegistry;

    public PlateauExplorer(InputReceiver inputReceiver, Area area, RoverStatusRegistry roverStatusRegistry)
    {
        _inputReceiver = inputReceiver;
        _area = area;
        _roverStatusRegistry = roverStatusRegistry;
    }

    public void Execute()
    {
        while (true)
        {
            ExecuteOnce();
        }
    }

    internal (bool isSuccess, string error) ExecuteOnce()
    {
        var roverCoordinatesAndDirection = _inputReceiver.GetRoverCoordinatesAndDirection(_roverId);
        var instructions = _inputReceiver.GetRoverInstructions(_roverId);

        var rover = GetOrCreateRover(_roverId, roverCoordinatesAndDirection);

        var roverController = new RoverController(rover, _area, instructions, _roverStatusRegistry);

        var (isSuccess, error) = roverController.Move();
        if (!isSuccess)
        {
            Console.WriteLine($"Error: {error}");
            AdvanceRoverId();
            Console.WriteLine($"{rover.Coordinates.X} {rover.Coordinates.Y} {rover.Direction.GetDescription()}");
            return (isSuccess, error);
        }

        Console.WriteLine($"Rover {rover.Id} moved successfully.");
        Console.WriteLine($"{rover.Coordinates.X} {rover.Coordinates.Y} {rover.Direction.GetDescription()}");

        AdvanceRoverId();
        return (isSuccess, error);
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
