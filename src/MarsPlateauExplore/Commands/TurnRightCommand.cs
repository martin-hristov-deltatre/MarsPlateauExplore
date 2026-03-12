using MarsPlateauExplore.Extensions;
using MarsPlateauExplore.Infrastructure;

namespace MarsPlateauExplore.Commands;

public class TurnRightCommand : IRoverCommand
{
    public Result Execute(RoverContext context)
    {
        context.Rover.TurnRight();
        Console.WriteLine($"Rover {context.Rover.Id} turned right and is now facing {context.Rover.Direction.GetDescription()}");
        return Result.Success();
    }
}
