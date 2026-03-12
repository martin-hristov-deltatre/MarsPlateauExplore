namespace MarsPlateauExplore.Application;

public class RoverStatusRegistry : IRoverStatusRegistry
{
    private readonly Dictionary<int, bool> _movingStatus = [];

    public void SetMoving(int roverId, bool isMoving) =>
        _movingStatus[roverId] = isMoving;

    public bool IsMoving(int roverId) =>
        _movingStatus.TryGetValue(roverId, out var isMoving) && isMoving;

    public bool AnyOtherRoverMoving(int currentRoverId) =>
        _movingStatus.Any(kvp => kvp.Key != currentRoverId && kvp.Value);
}
