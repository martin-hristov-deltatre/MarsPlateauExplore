using MarsPlateauExplore.Application;
using MarsPlateauExplore.Commands;
using MarsPlateauExplore.Enums;

namespace MarsPlateauExplore.UnitTests.Application;
public class CommandFactoryTests
{
    [Test]
    public void Create_WhenInstructionIsTurnLeft_ReturnsTurnLeftCommand()
    {
        var command = CommandFactory.Create(Instruction.TurnLeft);

        Assert.That(command, Is.InstanceOf<TurnLeftCommand>());
    }

    [Test]
    public void Create_WhenInstructionIsTurnRight_ReturnsTurnRightCommand()
    {
        var command = CommandFactory.Create(Instruction.TurnRight);

        Assert.That(command, Is.InstanceOf<TurnRightCommand>());
    }

    [Test]
    public void Create_WhenInstructionIsMoveForward_ReturnsMoveCommand()
    {
        var command = CommandFactory.Create(Instruction.MoveForward);

        Assert.That(command, Is.InstanceOf<MoveCommand>());
    }

    [Test]
    public void Create_WhenInstructionIsInvalid_ThrowsArgumentException()
    {
        var invalidInstruction = (Instruction)999;

        Assert.That(() => CommandFactory.Create(invalidInstruction), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void Create_ReturnsInstanceImplementingIRoverCommand()
    {
        var command = CommandFactory.Create(Instruction.MoveForward);

        Assert.That(command, Is.InstanceOf<IRoverCommand>());
    }

    [TestCase(Instruction.TurnLeft)]
    [TestCase(Instruction.TurnRight)]
    [TestCase(Instruction.MoveForward)]
    public void Create_WithValidInstruction_ReturnsNonNullCommand(Instruction instruction)
    {
        var command = CommandFactory.Create(instruction);

        Assert.That(command, Is.Not.Null);
    }
}
