using MarsPlateauExplore;
using MarsPlateauExplore.Application;
using MarsPlateauExplore.Domain;

internal class Program
{
    private static void Main(string[] args)
    {
        var inputReceiver = new InputReceiver();

        var area = new Area(inputReceiver.GetAreaSize());

        new PlateauExplorer(inputReceiver, area, new RoverStatusRegistry()).Execute(new RoverController());
    }
}