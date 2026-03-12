using MarsPlateauExplore.Enums;

namespace MarsPlateauExplore.Infrastructure.Parsers;

public static class InstructionsParser
{
    private static readonly Dictionary<string, Instruction> _instructionMap = new()
    {
        { "L", Instruction.TurnLeft },
        { "R", Instruction.TurnRight },
        { "M", Instruction.MoveForward }
    };

    public static Result TryParse(string? input, out List<Instruction> instructions)
    {
        instructions = [];

        if (string.IsNullOrWhiteSpace(input))
        {
            return Result.Failed("Invalid input. Please enter a sequence of instructions.");
        }

        foreach (var instructionLetter in input)
        {
            if (!_instructionMap.TryGetValue(instructionLetter.ToString(), out var insturction))
            {
                instructions = [];
                return Result.Failed($"Invalid instruction {instructionLetter}. Valid instructions are L, R, M.");
            }
            instructions.Add(insturction);
        }

        return Result.Success();
    }
}
