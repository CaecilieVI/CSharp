using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BDSA2017.Assignment04
{
    public class Queries
    {
        public static TrackInfo GetTrackInfo(int trackId)
        {
            using (Slot_Car_TournamentContext context = new Slot_Car_TournamentContext())
            {
                var track = context.Track.Find(trackId);

                var races = (from r in context.Race
                            where r.TrackId == track.TrackId
                            select r).ToList();

                var driver = (from d in context.CarInRace
                             where d.Race.Track == track && d.BestLap == track.BestTime
                             select d.Car).FirstOrDefault();

                var trackInfo = new TrackInfo();

                trackInfo.Id = track.TrackId;
                trackInfo.Name = track.Name;
                trackInfo.FastestLap = TimeSpan.FromMilliseconds(track.BestTime);
                trackInfo.FastestsCar = driver.Name;
                trackInfo.FastestsDriver = driver.DriverName;
                trackInfo.NumberOfRaces = (int)races.Count();



                return trackInfo;
            }
        }

        public static RaceInfo GetRaceInfo(int raceId)
        {
            using (Slot_Car_TournamentContext context = new Slot_Car_TournamentContext())
            {
                var race = context.Race.Find(raceId);

                var track = (from t in context.Track
                                 where t.TrackId == race.TrackId
                                 select t).FirstOrDefault();

                var cars = (from c in context.CarInRace
                               where c.RaceId == race.RaceId
                               select c).ToList();

                var carInfos = new List<CarInfo>();

                foreach (CarInRace cir in cars)
                {
                    var car = (from c in context.Car
                                where c.CarId == cir.CarId
                                select c).FirstOrDefault();

                    var carinfo = new CarInfo() {
                        CarId = cir.CarId,
                        CarName = car.Name,
                        DriverName = car.DriverName,
                        StartPosition = cir.StartPosition,
                        EndPosition = cir.EndPosition,
                        BestLapInTicks = (long)cir.BestLap * 10000,
                        TotalRaceTimeInTicks = (long)cir.TotalRaceTime * 10000
                    };

                    carInfos.Add(carinfo);
                }

                var raceInfo = new RaceInfo();

                raceInfo.RaceId = race.RaceId;
                raceInfo.TrackName = track.Name;
                raceInfo.TrackId = race.TrackId;
                raceInfo.PlannedStart = race.PlannedStartTime;
                raceInfo.PlannedEnd = race.PlannedEndTime;
                raceInfo.NumberOfLaps = race.NumberOfLaps;
                raceInfo.ActualStart = race.ActualStartTime;
                raceInfo.ActualEnd = race.ActualEndTime;
                raceInfo.Cars = carInfos;



                return raceInfo;
            }
        }
    }
}