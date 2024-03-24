using DangerousStreetRace.Race;

var raceController = new RaceController();

while (true)
{
    raceController.LaunchDuel();
    Console.WriteLine("Click enter to restart race...");
    Console.ReadLine();
}
