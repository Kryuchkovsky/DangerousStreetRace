using DangerousStreetRace.Cars;

namespace DangerousStreetRace.Race;

public class RaceController
{
    private List<Car> _cars;
    private List<Car> _participatingCars;

    private readonly Random _random;
    private float _currentTrackLength;
    private readonly float _minTrackLength = 600;
    private readonly float _maxTrackLength = 2400;
    
    private readonly int _frameDurationInMilliseconds = 15;
    private readonly float _deltaTime;

    public RaceController()
    {
        _cars = new List<Car>();
        _cars.Add(new GhostCar());
        _cars.Add(new JetCar());
        _cars.Add(new KabbalistCar());
        _cars.Add(new MechanicCar());
        _cars.Add(new MinerCar());
        _random = new Random();
        _deltaTime = _frameDurationInMilliseconds / 1000f;
    }

    public void LaunchDuel()
    {
        _participatingCars = new List<Car>();
        var carSelected = false;
        
        while (!carSelected)
        {
            Console.Clear();
            Console.WriteLine("---Race gets started!---");
            Console.WriteLine("---Choose your car:---");
            
            for (int i = 0; i < _cars.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {_cars[i].Name}");
            }
        
            Console.WriteLine($"Write car number...");
            var input = Console.ReadLine();

            if (!String.IsNullOrEmpty(input) && int.TryParse(input, out var playerCarNumber) && playerCarNumber > 0 && playerCarNumber <= _cars.Count)
            {
                carSelected = true;
                _participatingCars.Add(_cars[playerCarNumber - 1].Clone() as Car);
                var botCarIndex = _random.Next(0, _cars.Count - 1);
                _participatingCars.Add(_cars[botCarIndex].Clone() as Car);

                if (playerCarNumber - 1 == botCarIndex)
                {
                    _participatingCars[0].RandomizeSkillFactors();
                    _participatingCars[1].RandomizeSkillFactors();
                }
                
                Console.WriteLine($"Your car is '{_participatingCars[0].Name}'");

                for (int i = 1; i < _participatingCars.Count; i++)
                {
                    Console.WriteLine($"Rival {i} has '{_participatingCars[i].Name}' car");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Incorrect value, try again!");
            }
        }
        
        _currentTrackLength = float.Lerp(_minTrackLength, _maxTrackLength, _random.NextSingle());
        Console.WriteLine($"Track distance: {_currentTrackLength}");
        Console.WriteLine("Press enter to start the race!");
        Console.ReadLine();
        StartRace();
    }

    private void StartRace()
    {
        var raceIsOn = true;
        var numberOfLiving = 0;

        while (raceIsOn)
        {
            Console.Clear();

            numberOfLiving = 0;
            
            for (int i = 0; i < _participatingCars.Count; i++)
            {
                var closestRival = FindClosestRival(i);
                _participatingCars[i].Rival = closestRival;
                _participatingCars[i].UpdateTime(_deltaTime);
            }
            
            _participatingCars = _participatingCars.OrderByDescending(c => c.PassedDistance).ToList();
            
            for (int j = 0; j < _participatingCars.Count; j++)
            {
                numberOfLiving += _participatingCars[j].IsDead ? 0 : 1;
                _participatingCars[j].Position = j + 1;

                Console.WriteLine($"Position: {j + 1}. Car: {_participatingCars[j].Name}");
                Console.WriteLine($"Passed distance: {(int)_participatingCars[j].PassedDistance}");
                Console.WriteLine($"Speed: {(int)_participatingCars[j].CurrentSpeed}.");
                Console.WriteLine($"Health: {(int)_participatingCars[j].CurrentHealth}.");
                Console.WriteLine($"Energy: {(int)_participatingCars[j].CurrentEnergy}");
                Console.WriteLine($"--------------------------------------------------");
            }

            raceIsOn = numberOfLiving > 1 && _participatingCars[0].PassedDistance < _currentTrackLength;
            
            Thread.Sleep(_frameDurationInMilliseconds);
        }

        var winner = numberOfLiving == 1 ? _participatingCars.First(c => !c.IsDead) : _participatingCars[0];
        Console.WriteLine($"---Winner is {winner.Name}---");
    }

    private Car FindClosestRival(int carIndex)
    {
        var rivalIndex = 0;
        var minDistance = float.MaxValue;

        for (int i = 0; i < _participatingCars.Count(); i++)
        {
            if (i == carIndex) continue;

            var distance = float.Abs(_participatingCars[i].PassedDistance - _participatingCars[carIndex].PassedDistance);
            
            if (distance < minDistance)
            {
                rivalIndex = i;
                minDistance = distance;
            }
        }

        return _participatingCars[rivalIndex];
    }
}