namespace MarsPlateauExplore.Application;
public interface IRoverStatusRegistry
{
    bool AnyOtherRoverMoving(int currentRoverId);
    bool IsMoving(int roverId);
    void SetMoving(int roverId, bool isMoving);
}
