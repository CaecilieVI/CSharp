using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace BDSA2017.Assignment04
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using(var context = new Slot_Car_TournamentContext())
            {
                Seed(context);
                context.SaveChanges();
            }
        }

        public static void Seed(Slot_Car_TournamentContext context)
        {
            var cs = from c in context.Car
                     select c;

            if (cs.ToList().Count <= 0)     //Hack/cheat to check if things from Seed are already added to the database.
            {                               //This should be implemented better given more time...
                AddTracks(context);         //Maybe import libraries to implement "SetUp" and "TearDown" for testing
                AddRaces(context);          //So the everything is added before all tests, and then removed after.
                AddCars(context);
                AddCarsToRace(context);
            }
        }

        public static void Clear(Slot_Car_TournamentContext context)
        {
            context.Database.ExecuteSqlCommand("DELETE * FROM CarInRace");
            context.SaveChanges();
        
            context.Database.ExecuteSqlCommand("DELETE * FROM Race");
            context.SaveChanges();

            context.Database.ExecuteSqlCommand("DELETE * FROM Cars");
            context.SaveChanges();

            context.Database.ExecuteSqlCommand("DELETE * FROM Track");
            context.SaveChanges();
        }

        public static void AddTracks(Slot_Car_TournamentContext context)
        {
            var TrackIndiana = new Track
            {
                Name = "Indianapolis Race Track",
                LengthMeter = 2782,
                BestTime = 29871,
                MaxCars = 40,
                TrackId = 1
            };
            context.Add(TrackIndiana);

            var TrackMonaco = new Track
            {
                Name = "Monaco Race Track",
                LengthMeter = 4929,
                BestTime = 31928,
                MaxCars = 26,
                TrackId = 2
            };
            context.Add(TrackMonaco);
        }

        public static void AddRaces(Slot_Car_TournamentContext context)
        {
            var RaceIndianaOne = new Race
            {
                TrackId = 1,
                RaceId = 1,
                NumberOfLaps = 100,
                PlannedStartTime = new DateTime(2017, 9, 21, 12, 0, 0),
                ActualStartTime = new DateTime(2017, 9, 21, 12, 0, 0),
                PlannedEndTime = new DateTime(2017, 9, 21, 13, 0, 0),
                ActualEndTime = new DateTime(2017, 9, 21, 13, 30, 0)
            };
            context.Add(RaceIndianaOne);
            
            var RaceIndianaTwo = new Race
            {
                TrackId = 1,
                RaceId = 2,
                NumberOfLaps = 150,
                PlannedStartTime = new DateTime(2017, 9, 22, 16, 0, 0),
                ActualStartTime = new DateTime(2017, 9, 22, 16, 0, 0),
                PlannedEndTime = new DateTime(2017, 9, 22, 17, 30, 0),
                ActualEndTime = new DateTime(2017, 9, 22, 17, 30, 0)
            };
            context.Add(RaceIndianaTwo);
           
            var RaceMonacoOne = new Race
            {
                TrackId = 2,
                RaceId = 3,
                NumberOfLaps = 80,
                PlannedStartTime = new DateTime(2017, 9, 22, 16, 0, 0),
                ActualStartTime = new DateTime(2017, 9, 22, 16, 0, 0),
                PlannedEndTime = new DateTime(2017, 9, 22, 18, 30, 0),
                ActualEndTime = new DateTime(2017, 9, 22, 18, 34, 0)
            };
            context.Add(RaceMonacoOne);
        }

        public static void AddCars(Slot_Car_TournamentContext context)
        {
            var car1 = new Car { Name = "The Car", DriverName = "Marston", CarId = 1};
            context.Add(car1);
            
            var car2 = new Car { Name = "ProX", DriverName = "The Stig", CarId = 2};
            context.Add(car2);
           
            var car3 = new Car { Name = "The Best Car", DriverName = "Jeremy", CarId = 3};
            context.Add(car3);
            
            var car4 = new Car { Name = "The Car That Ran Out Of Names", DriverName = "Chris", CarId = 4};
            context.Add(car4);
        }

        public static void AddCarsToRace(Slot_Car_TournamentContext context)
        {
            var carInRaceOne = new CarInRace
            {
                RaceId = 1,
                CarId = 1,
                BestLap = 29867,
                EndPosition = 2,
                StartPosition = 1,
                TotalRaceTime = 192835387
            };
            context.Add(carInRaceOne);
          
            var carInRaceTwo = new CarInRace
            {
                RaceId = 1,
                CarId = 2,
                BestLap = 29871,
                EndPosition = 1,
                StartPosition = 2,
                TotalRaceTime = 19255387
            };
            context.Add(carInRaceTwo);
            
            var carInRaceThree = new CarInRace
            {
                RaceId = 2,
                CarId = 2,
                BestLap = 29999,
                EndPosition = 2,
                StartPosition = 2,
                TotalRaceTime = 162435387
            };
            context.Add(carInRaceThree);
            
            var carInRaceFour = new CarInRace
            {
                RaceId = 2,
                CarId = 1,
                BestLap = 26147,
                EndPosition = 1,
                StartPosition = 1,
                TotalRaceTime = 192863524
            };
            context.Add(carInRaceFour);
            
            var carInRaceFive = new CarInRace
            {
                RaceId = 3,
                CarId = 3,
                BestLap = 42315,
                EndPosition = 2,
                StartPosition = 1,
                TotalRaceTime = 192888724
            };
            context.Add(carInRaceFive);
            
            var carInRaceSix = new CarInRace
            {
                RaceId = 3,
                CarId = 4,
                BestLap = 31928,
                EndPosition = 1,
                StartPosition = 2,
                TotalRaceTime = 192882924
            };
            context.Add(carInRaceSix);
        }
    }
}