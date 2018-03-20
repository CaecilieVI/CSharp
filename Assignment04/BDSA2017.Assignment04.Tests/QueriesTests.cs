using Xunit;
using System;
using System.Collections.Generic;

namespace BDSA2017.Assignment04.Tests
{
    public class QueriesTests
    {
        Slot_Car_TournamentContext context = new Slot_Car_TournamentContext();

        [Fact(DisplayName = "GetTrackInfo_given_trackid_returns_trackinfo")]
        public void GetTrackInfo_given_trackid_returns_trackinfo() {

            Program.Main(new string[0]);

            var expected = new TrackInfo()
            {
                Id = 1,
                Name = "Indianapolis Race Track",
                FastestLap = TimeSpan.FromMilliseconds(29871),
                FastestsCar = "ProX",
                FastestsDriver = "The Stig",
                NumberOfRaces = 2
            };
            var actual = Queries.GetTrackInfo(expected.Id);

            Assert.Equal(expected, actual);
        }

        [Fact(DisplayName = "GetRaceInfo_given_raceid_returns_raceinfo")]
        public void GetRaceInfo_given_raceid_returns_raceinfo()
        {
            var listOfCars = new List<CarInfo>();

            var carInfoOne = new CarInfo()
            {
                CarId = 1,
                CarName = "The Car",
                DriverName = "Marston",
                StartPosition = 1,
                EndPosition = 2,
                BestLapInTicks = (long)29867 * 10000,
                TotalRaceTimeInTicks = (long)192835387 * 10000
            };

            var carInfoTwo = new CarInfo()
            {
                CarId = 2,
                CarName = "ProX",
                DriverName = "The Stig",
                StartPosition = 2,
                EndPosition = 1,
                BestLapInTicks = (long)29871 * 10000,
                TotalRaceTimeInTicks = (long)19255387 * 10000
            };
            
            listOfCars.Add(carInfoOne);
            listOfCars.Add(carInfoTwo);

            var expected = new RaceInfo()
            {
                RaceId = 1,
                TrackId = 1,
                TrackName = "Indianapolis Race Track",
                NumberOfLaps = 100,
                PlannedStart = new DateTime(2017, 9, 21, 12, 0, 0),
                PlannedEnd = new DateTime(2017, 9, 21, 13, 0, 0),
                ActualStart = new DateTime(2017, 9, 21, 12, 0, 0),
                ActualEnd = new DateTime(2017, 9, 21, 13, 30, 0),
            };
            var actual = Queries.GetRaceInfo(expected.RaceId);

            var actualCars = new List<CarInfo>();

            foreach(CarInfo ci in actual.Cars)
            {
                actualCars.Add(ci);
            }

            actual.Cars = null;

            Console.WriteLine(expected.Cars);
                
            Assert.Equal(expected, actual);

            actual.Cars = actualCars;
            expected.Cars = listOfCars;

            Assert.Equal(expected.Cars, actual.Cars);
        }
    }
}