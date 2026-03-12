using MarsPlateauExplore.Commands;
using MarsPlateauExplore.Domain;
using MarsPlateauExplore.Enums;
using MarsPlateauExplore.Infrastructure;

namespace MarsPlateauExplore.Application;

public class RoverController : IRoverController
{
    public Result Execute(Rover rover, List<Instruction> instructions, Area area, IRoverStatusRegistry statusRegistry)
    {
        statusRegistry.SetMoving(rover.Id, true);

        foreach (var instruction in instructions)
        {
            var command = CommandFactory.Create(instruction);

            if (statusRegistry.AnyOtherRoverMoving(rover.Id))
            {
                Console.WriteLine($"Rover {rover.Id} is waiting for another rover to finish moving.");
                Thread.Sleep(100);
                continue;
            }

            var result = command.Execute(new RoverContext(rover, area));
            if (!result.IsSuccess)
            {
                statusRegistry.SetMoving(rover.Id, false);
                return result;
            }
        }

        statusRegistry.SetMoving(rover.Id, false);
        return Result.Success();
    }
}