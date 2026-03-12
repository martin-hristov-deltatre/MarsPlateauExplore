using MarsPlateauExplore.Commands;
using MarsPlateauExplore.Enums;

namespace MarsPlateauExplore.Application;

public class CommandFactory
{
    public static IRoverCommand Create(Instruction instruction)
    {
        return instruction switch
        {
            Instruction.TurnLeft => new TurnLeftCommand(),
            Instruction.TurnRight => new TurnRightCommand(),
            Instruction.MoveForward => new MoveCommand(),
            _ => throw new ArgumentException("Invalid command")
        };
    }
}
