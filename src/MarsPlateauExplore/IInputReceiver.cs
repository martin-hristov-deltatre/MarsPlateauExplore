using MarsPlateauExplore.Domain;
using MarsPlateauExplore.Enums;

namespace MarsPlateauExplore;
public interface IInputReceiver
{
    Coordinates GetAreaSize();
    (Coordinates Coordinates, Direction Direction) GetRoverCoordinatesAndDirection(int roverId);
    List<Instruction> GetRoverInstructions(int roverId);
}
