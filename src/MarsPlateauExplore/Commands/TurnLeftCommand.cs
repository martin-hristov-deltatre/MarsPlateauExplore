using MarsPlateauExplore.Extensions;
using MarsPlateauExplore.Infrastructure;

namespace MarsPlateauExplore.Commands;

public class TurnLeftCommand : IRoverCommand
{
    public Result Execute(RoverContext context)
    {
        context.Rover.TurnLeft();
        Console.WriteLine($"Rover {context.Rover.Id} turned left and is now facing {context.Rover.Direction.GetDescription()}");
        return Result.Success();
    }
}
