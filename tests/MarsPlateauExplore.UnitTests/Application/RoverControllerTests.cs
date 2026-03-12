using MarsPlateauExplore.Application;
using MarsPlateauExplore.Domain;
using MarsPlateauExplore.Enums;
using Moq;

namespace MarsPlateauExplore.UnitTests.Application;
public class RoverControllerTests
{
    private RoverController _controller;
    private Mock<IRoverStatusRegistry> _statusRegistryMock;
    private Rover _rover;
    private Area _area;

    [SetUp]
    public void SetUp()
    {
        _controller = new RoverController();
        _statusRegistryMock = new Mock<IRoverStatusRegistry>();
        _rover = new Rover(1, new Coordinates(1, 1), Direction.North);
        _area = new Area(new Coordinates(5, 5));
    }

    [Test]
    public void Execute_WithEmptyInstructions_ReturnsSuccess()
    {
        // Arrange
        var instructions = new List<Instruction>();

        // Act
        var result = _controller.Execute(_rover, instructions, _area, _statusRegistryMock.Object);

        // Assert
        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public void Execute_WithEmptyInstructions_SetsMovingTrueAtStartAndFalseAtEnd()
    {
        // Arrange
        var instructions = new List<Instruction>();

        // Act
        _controller.Execute(_rover, instructions, _area, _statusRegistryMock.Object);

        // Assert
        _statusRegistryMock.Verify(r => r.SetMoving(_rover.Id, true), Times.Once);
        _statusRegistryMock.Verify(r => r.SetMoving(_rover.Id, false), Times.Once);
    }

    [Test]
    public void Execute_WithTurnLeftInstruction_ReturnsSuccess()
    {
        // Arrange
        var instructions = new List<Instruction> { Instruction.TurnLeft };
        _statusRegistryMock.Setup(r => r.AnyOtherRoverMoving(_rover.Id)).Returns(false);

        // Act
        var result = _controller.Execute(_rover, instructions, _area, _statusRegistryMock.Object);

        // Assert
        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public void Execute_WithTurnRightInstruction_ReturnsSuccess()
    {
        // Arrange
        var instructions = new List<Instruction> { Instruction.TurnRight };
        _statusRegistryMock.Setup(r => r.AnyOtherRoverMoving(_rover.Id)).Returns(false);

        // Act
        var result = _controller.Execute(_rover, instructions, _area, _statusRegistryMock.Object);

        // Assert
        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public void Execute_WithMoveForwardInstruction_ReturnsSuccess_WhenWithinBounds()
    {
        // Arrange
        var instructions = new List<Instruction> { Instruction.MoveForward };
        _statusRegistryMock.Setup(r => r.AnyOtherRoverMoving(_rover.Id)).Returns(false);

        // Act
        var result = _controller.Execute(_rover, instructions, _area, _statusRegistryMock.Object);

        // Assert
        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public void Execute_WhenAnotherRoverIsMoving_SkipsInstructionExecution()
    {
        // Arrange
        var instructions = new List<Instruction> { Instruction.MoveForward };
        _statusRegistryMock.Setup(r => r.AnyOtherRoverMoving(_rover.Id)).Returns(true);

        var initialCoordinates = new Coordinates(_rover.Coordinates.X, _rover.Coordinates.Y);

        // Act
        _controller.Execute(_rover, instructions, _area, _statusRegistryMock.Object);

        // Assert - rover position should remain unchanged since instruction was skipped
        Assert.That(_rover.Coordinates.X, Is.EqualTo(initialCoordinates.X));
        Assert.That(_rover.Coordinates.Y, Is.EqualTo(initialCoordinates.Y));
    }

    [Test]
    public void Execute_WhenMoveForwardFails_ReturnsFailure()
    {
        // Arrange - place rover at boundary so MoveForward fails
        var roverAtBoundary = new Rover(2, new Coordinates(5, 5), Direction.North);
        var instructions = new List<Instruction> { Instruction.MoveForward };
        _statusRegistryMock.Setup(r => r.AnyOtherRoverMoving(roverAtBoundary.Id)).Returns(false);

        // Act
        var result = _controller.Execute(roverAtBoundary, instructions, _area, _statusRegistryMock.Object);

        // Assert
        Assert.That(result.IsSuccess, Is.False);
    }

    [Test]
    public void Execute_WhenCommandFails_SetsMovingToFalse()
    {
        // Arrange - place rover at boundary so MoveForward fails
        var roverAtBoundary = new Rover(2, new Coordinates(5, 5), Direction.North);
        var instructions = new List<Instruction> { Instruction.MoveForward };
        _statusRegistryMock.Setup(r => r.AnyOtherRoverMoving(roverAtBoundary.Id)).Returns(false);

        // Act
        _controller.Execute(roverAtBoundary, instructions, _area, _statusRegistryMock.Object);

        // Assert
        _statusRegistryMock.Verify(r => r.SetMoving(roverAtBoundary.Id, false), Times.Once);
    }

    [Test]
    public void Execute_WithMultipleInstructions_ExecutesAllSuccessfully()
    {
        // Arrange
        var instructions = new List<Instruction>
        {
            Instruction.TurnLeft,
            Instruction.TurnRight,
            Instruction.MoveForward
        };
        _statusRegistryMock.Setup(r => r.AnyOtherRoverMoving(_rover.Id)).Returns(false);

        // Act
        var result = _controller.Execute(_rover, instructions, _area, _statusRegistryMock.Object);

        // Assert
        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public void Execute_SetsMovingTrueBeforeExecutingInstructions()
    {
        // Arrange
        var instructions = new List<Instruction> { Instruction.TurnLeft };
        var order = new List<string>();

        _statusRegistryMock
            .Setup(r => r.SetMoving(_rover.Id, true))
            .Callback(() => order.Add("SetMoving(true)"));

        _statusRegistryMock
            .Setup(r => r.AnyOtherRoverMoving(_rover.Id))
            .Callback(() => order.Add("AnyOtherRoverMoving"))
            .Returns(false);

        // Act
        _controller.Execute(_rover, instructions, _area, _statusRegistryMock.Object);

        // Assert
        Assert.That(order.IndexOf("SetMoving(true)"), Is.LessThan(order.IndexOf("AnyOtherRoverMoving")));
    }
}
