using MarsPlateauExplore.Infrastructure;

namespace MarsPlateauExplore.Commands;

public interface IRoverCommand
{
    Result Execute(RoverContext context);
}
