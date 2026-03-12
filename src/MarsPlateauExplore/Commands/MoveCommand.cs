using MarsPlateauExplore.Infrastructure;

namespace MarsPlateauExplore.Commands;

public class MoveCommand : IRoverCommand
{
    public Result Execute(RoverContext context)
    {
        var next = context.Rover.GetNextCoordinates();

        var result = context.Area.TryMove(context.Rover, next);

        if (!result.IsSuccess)
            return Result.Failed(result.Error);

        context.Rover.MoveTo();

        return Result.Success();
    }
}
