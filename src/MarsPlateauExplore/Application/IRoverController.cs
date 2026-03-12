using MarsPlateauExplore.Domain;
using MarsPlateauExplore.Enums;
using MarsPlateauExplore.Infrastructure;

namespace MarsPlateauExplore.Application;
public interface IRoverController
{
    Result Execute(Rover rover, List<Instruction> instructions, Area area, IRoverStatusRegistry statusRegistry);
}
