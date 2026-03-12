using MarsPlateauExplore.Application;
using MarsPlateauExplore.Domain;
using MarsPlateauExplore.Enums;
using MarsPlateauExplore.Infrastructure;
using Moq;

namespace MarsPlateauExplore.Tests.Application;

public class PlateauExplorerTests
{
    private Mock<IInputReceiver> _inputReceiverMock;
    private Mock<IRoverController> _roverControllerMock;
    private Mock<IRoverStatusRegistry> _roverStatusRegistryMock;
    private Area _area;
    private PlateauExplorer _plateauExplorer;

    [SetUp]
    public void SetUp()
    {
        _inputReceiverMock = new Mock<IInputReceiver>();
        _roverControllerMock = new Mock<IRoverController>();
        _roverStatusRegistryMock = new Mock<IRoverStatusRegistry>();

        _area = new Area(new Coordinates(5, 5));

        _plateauExplorer = new PlateauExplorer(
            _inputReceiverMock.Object,
            _area,
            _roverStatusRegistryMock.Object
        );
    }

    [Test]
    public void ExecuteOnce_WhenControllerSucceeds_ReturnsSuccess()
    {
        // Arrange
        var coordinates = new Coordinates(1, 2);
        var instructions = new List<Instruction> { Instruction.MoveForward };

        _inputReceiverMock
            .Setup(r => r.GetRoverCoordinatesAndDirection(1))
            .Returns((coordinates, Direction.North));

        _inputReceiverMock
            .Setup(r => r.GetRoverInstructions(1))
            .Returns(instructions);

        _roverControllerMock
            .Setup(c => c.Execute(It.IsAny<Rover>(), instructions, _area, _roverStatusRegistryMock.Object))
            .Returns(Result.Success());

        // Act
        var result = _plateauExplorer.ExecuteOnce(_roverControllerMock.Object);

        // Assert
        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public void ExecuteOnce_WhenControllerFails_ReturnsFailure()
    {
        // Arrange
        var coordinates = new Coordinates(1, 2);
        var instructions = new List<Instruction> { Instruction.MoveForward };
        var errorMessage = "Rover out of bounds";

        _inputReceiverMock
            .Setup(r => r.GetRoverCoordinatesAndDirection(1))
            .Returns((coordinates, Direction.North));

        _inputReceiverMock
            .Setup(r => r.GetRoverInstructions(1))
            .Returns(instructions);

        _roverControllerMock
            .Setup(c => c.Execute(It.IsAny<Rover>(), instructions, _area, _roverStatusRegistryMock.Object))
            .Returns(Result.Failed(errorMessage));

        // Act
        var result = _plateauExplorer.ExecuteOnce(_roverControllerMock.Object);

        // Assert
        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.Error, Is.EqualTo(errorMessage));
    }

    [Test]
    public void ExecuteOnce_WhenCalledTwice_CreatesTwoDistinctRovers()
    {
        // Arrange
        var rover1Coords = new Coordinates(1, 2);
        var rover2Coords = new Coordinates(3, 3);
        var instructions = new List<Instruction> { Instruction.TurnLeft };

        _inputReceiverMock
            .Setup(r => r.GetRoverCoordinatesAndDirection(1))
            .Returns((rover1Coords, Direction.North));

        _inputReceiverMock
            .Setup(r => r.GetRoverCoordinatesAndDirection(2))
            .Returns((rover2Coords, Direction.East));

        _inputReceiverMock
            .Setup(r => r.GetRoverInstructions(It.IsAny<int>()))
            .Returns(instructions);

        _roverControllerMock
            .Setup(c => c.Execute(It.IsAny<Rover>(), instructions, _area, _roverStatusRegistryMock.Object))
            .Returns(Result.Success());

        // Act
        _plateauExplorer.ExecuteOnce(_roverControllerMock.Object); // Rover 1
        _plateauExplorer.ExecuteOnce(_roverControllerMock.Object); // Rover 2

        // Assert
        _roverControllerMock.Verify(
            c => c.Execute(It.Is<Rover>(r => r.Id == 1), instructions, _area, _roverStatusRegistryMock.Object),
            Times.Once
        );
        _roverControllerMock.Verify(
            c => c.Execute(It.Is<Rover>(r => r.Id == 2), instructions, _area, _roverStatusRegistryMock.Object),
            Times.Once
        );
    }

    [Test]
    public void ExecuteOnce_WhenRoverAlreadyExists_ReusesExistingRover()
    {
        // Arrange
        var rover1Coords = new Coordinates(1, 2);
        var rover2Coords = new Coordinates(3, 3);
        var instructions = new List<Instruction> { Instruction.TurnRight };

        _inputReceiverMock
            .Setup(r => r.GetRoverCoordinatesAndDirection(1))
            .Returns((rover1Coords, Direction.North));

        _inputReceiverMock
            .Setup(r => r.GetRoverCoordinatesAndDirection(2))
            .Returns((rover2Coords, Direction.East));

        _inputReceiverMock
            .Setup(r => r.GetRoverInstructions(It.IsAny<int>()))
            .Returns(instructions);

        _roverControllerMock
            .Setup(c => c.Execute(It.IsAny<Rover>(), instructions, _area, _roverStatusRegistryMock.Object))
            .Returns(Result.Success());

        // Act
        _plateauExplorer.ExecuteOnce(_roverControllerMock.Object); // Rover 1 - created
        _plateauExplorer.ExecuteOnce(_roverControllerMock.Object); // Rover 2 - created
        _plateauExplorer.ExecuteOnce(_roverControllerMock.Object); // Rover 1 - reused

        // Assert - Rover 1 controller is called twice but created once (same instance reused)
        _roverControllerMock.Verify(
            c => c.Execute(It.Is<Rover>(r => r.Id == 1), instructions, _area, _roverStatusRegistryMock.Object),
            Times.Exactly(2)
        );
    }

    [Test]
    public void ExecuteOnce_CallsGetRoverCoordinatesAndDirection_WithCorrectRoverId()
    {
        // Arrange
        var coordinates = new Coordinates(0, 0);
        var instructions = new List<Instruction>();

        _inputReceiverMock
            .Setup(r => r.GetRoverCoordinatesAndDirection(1))
            .Returns((coordinates, Direction.North));

        _inputReceiverMock
            .Setup(r => r.GetRoverInstructions(1))
            .Returns(instructions);

        _roverControllerMock
            .Setup(c => c.Execute(It.IsAny<Rover>(), instructions, _area, _roverStatusRegistryMock.Object))
            .Returns(Result.Success());

        // Act
        _plateauExplorer.ExecuteOnce(_roverControllerMock.Object);

        // Assert
        _inputReceiverMock.Verify(r => r.GetRoverCoordinatesAndDirection(1), Times.Once);
        _inputReceiverMock.Verify(r => r.GetRoverInstructions(1), Times.Once);
    }

    [Test]
    public void ExecuteOnce_CallsRoverController_WithCorrectParameters()
    {
        // Arrange
        var coordinates = new Coordinates(2, 3);
        var instructions = new List<Instruction> { Instruction.MoveForward, Instruction.TurnLeft };

        _inputReceiverMock
            .Setup(r => r.GetRoverCoordinatesAndDirection(1))
            .Returns((coordinates, Direction.South));

        _inputReceiverMock
            .Setup(r => r.GetRoverInstructions(1))
            .Returns(instructions);

        _roverControllerMock
            .Setup(c => c.Execute(It.IsAny<Rover>(), instructions, _area, _roverStatusRegistryMock.Object))
            .Returns(Result.Success());

        // Act
        _plateauExplorer.ExecuteOnce(_roverControllerMock.Object);

        // Assert
        _roverControllerMock.Verify(
            c => c.Execute(
                It.Is<Rover>(r => r.Coordinates.X == 2 && r.Coordinates.Y == 3 && r.Direction == Direction.South),
                instructions,
                _area,
                _roverStatusRegistryMock.Object
            ),
            Times.Once
        );
    }
}
