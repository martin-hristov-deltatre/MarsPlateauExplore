using MarsPlateauExplore.Enums;
using MarsPlateauExplore.Infrastructure.Parsers;

namespace MarsPlateauExplore.UnitTests.Infrastructure.Parsers;
public class InstructionsParserTests
{
    [Test]
    public void TryParse_WhenInputIsNull_ReturnsFailedResult()
    {
        var result = InstructionsParser.TryParse(null, out var instructions);

        Assert.That(result.IsFailure, Is.True);
        Assert.That(result.Error, Is.EqualTo("Invalid input. Please enter a sequence of instructions."));
        Assert.That(instructions, Is.Empty);
    }

    [Test]
    public void TryParse_WhenInputIsEmpty_ReturnsFailedResult()
    {
        var result = InstructionsParser.TryParse(string.Empty, out var instructions);

        Assert.That(result.IsFailure, Is.True);
        Assert.That(result.Error, Is.EqualTo("Invalid input. Please enter a sequence of instructions."));
        Assert.That(instructions, Is.Empty);
    }

    [Test]
    public void TryParse_WhenInputIsWhiteSpace_ReturnsFailedResult()
    {
        var result = InstructionsParser.TryParse("   ", out var instructions);

        Assert.That(result.IsFailure, Is.True);
        Assert.That(result.Error, Is.EqualTo("Invalid input. Please enter a sequence of instructions."));
        Assert.That(instructions, Is.Empty);
    }

    [Test]
    public void TryParse_WhenInputContainsInvalidInstruction_ReturnsFailedResult()
    {
        var result = InstructionsParser.TryParse("LXR", out var instructions);

        Assert.That(result.IsFailure, Is.True);
        Assert.That(result.Error, Is.EqualTo("Invalid instruction X. Valid instructions are L, R, M."));
        Assert.That(instructions, Is.Empty);
    }

    [Test]
    public void TryParse_WhenInputIsValidSingleInstruction_L_ReturnsSuccessWithTurnLeft()
    {
        var result = InstructionsParser.TryParse("L", out var instructions);

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(instructions, Has.Count.EqualTo(1));
        Assert.That(instructions[0], Is.EqualTo(Instruction.TurnLeft));
    }

    [Test]
    public void TryParse_WhenInputIsValidSingleInstruction_R_ReturnsSuccessWithTurnRight()
    {
        var result = InstructionsParser.TryParse("R", out var instructions);

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(instructions, Has.Count.EqualTo(1));
        Assert.That(instructions[0], Is.EqualTo(Instruction.TurnRight));
    }

    [Test]
    public void TryParse_WhenInputIsValidSingleInstruction_M_ReturnsSuccessWithMoveForward()
    {
        var result = InstructionsParser.TryParse("M", out var instructions);

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(instructions, Has.Count.EqualTo(1));
        Assert.That(instructions[0], Is.EqualTo(Instruction.MoveForward));
    }

    [Test]
    public void TryParse_WhenInputIsValidSequence_ReturnsSuccessWithCorrectInstructions()
    {
        var result = InstructionsParser.TryParse("LRMMLR", out var instructions);

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(instructions, Has.Count.EqualTo(6));
        Assert.That(instructions, Is.EqualTo(new List<Instruction>
        {
            Instruction.TurnLeft,
            Instruction.TurnRight,
            Instruction.MoveForward,
            Instruction.MoveForward,
            Instruction.TurnLeft,
            Instruction.TurnRight
        }));
    }

    [Test]
    public void TryParse_WhenInputIsLowerCase_ReturnsFailedResult()
    {
        var result = InstructionsParser.TryParse("lrm", out var instructions);

        Assert.That(result.IsFailure, Is.True);
        Assert.That(result.Error, Is.EqualTo("Invalid instruction l. Valid instructions are L, R, M."));
        Assert.That(instructions, Is.Empty);
    }
}
