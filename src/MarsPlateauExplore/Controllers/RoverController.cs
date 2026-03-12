using MarsPlateauExplore.Domain;
using MarsPlateauExplore.Enums;
using MarsPlateauExplore.Extensions;

namespace MarsPlateauExplore.Controllers;

public class RoverController(Rover rover, Area area, List<Instruction> instructions, RoverStatusRegistry statusRegistry)
{
    public (bool isSuccess, string error) Move()
    {
        statusRegistry.SetMoving(rover.Id, true);

        foreach (var instruction in instructions)
        {
            if (statusRegistry.AnyOtherRoverMoving(rover.Id))
            {
                Console.WriteLine($"Rover {rover.Id} is waiting for another rover to finish moving.");
                Thread.Sleep(1000);
            }

            if (instruction != Instruction.MoveForward)
            {
                Turn(instruction);
                continue;
            }

            var (success, error) = MoveForward();
            if (!success)
            {
                statusRegistry.SetMoving(rover.Id, false);
                return (false, error);
            }
        }

        statusRegistry.SetMoving(rover.Id, false);
        return (true, string.Empty);
    }

    private void Turn(Instruction instruction)
    {
        if (instruction == Instruction.TurnLeft)
        {
            rover.TurnLeft();
            Console.WriteLine($"Rover {rover.Id} turned left and is now facing {rover.Direction.GetDescription()}");
        }
        else
        {
            rover.TurnRight();
            Console.WriteLine($"Rover {rover.Id} turned right and is now facing {rover.Direction.GetDescription()}");
        }
    }

    private (bool isSuccess, string error) MoveForward()
    {
        var next = rover.GetNextCoordinates();

        if (!area.IsWithinBounds(next))
            return (false, $"Rover {rover.Id} cannot move to ({next.X}, {next.Y}) as it is out of bounds.");

        if (area.IsOccupied(rover.Id, next))
            return (false, $"Rover {rover.Id} cannot move to ({next.X}, {next.Y}) as it is occupied by another rover.");

        area.UpsertOccupiedCoordinates(rover.Id, next);
        rover.MoveTo(next);

        Console.WriteLine($"Rover {rover.Id} moved to ({next.X}, {next.Y}) facing {rover.Direction.GetDescription()}");

        return (true, string.Empty);
    }
}