using MarsPlateauExplore.Enums;

namespace MarsPlateauExplore.Parsers;

public static class InstructionsParser
{
    private static readonly Dictionary<string, Instruction> _instructionMap = new()
    {
        { "L", Instruction.TurnLeft },
        { "R", Instruction.TurnRight },
        { "M", Instruction.MoveForward }
    };

    public static (bool isSuccess, string error) TryParse(string? input, out List<Instruction> instructions)
    {
        instructions = [];

        if (string.IsNullOrWhiteSpace(input))
        {
            return (false, "Invalid input. Please enter a sequence of instructions.");
        }

        foreach (var instructionLetter in input)
        {
            if (!_instructionMap.TryGetValue(instructionLetter.ToString(), out var insturction))
            {
                return (false, $"Invalid instruction {instructionLetter}. Valid instructions are L, R, M.");
            }
            instructions.Add(insturction);
        }

        return (true, string.Empty);
    }
}
