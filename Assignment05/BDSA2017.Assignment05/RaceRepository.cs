using BDSA2017.Assignment05.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using BDSA2017.Assignment05.DTOs;
using BDSA2017.Assignment05.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BDSA2017.Assignment05
{
    public class RaceRepository : IRaceRepository
    {
        private readonly ISlotCarContext _context;
        public RaceRepository(ISlotCarContext context)
        {
            _context = context;
        }

        public (bool ok, string error) AddCarToRace(int carId, int raceId, int? startPosition = null)
        {
            var car = _context.Cars.Find(carId);
            var race = _context.Races.Find(raceId);
            if (car == null) return (false, "Couldn't find car");
            if (race == null) return (false, "Couldn't find race");

            var currentNumberOfCars = (from c in _context.CarsInRace
                                       where c.Race == race
                                       select c).Count();
            if (race.ActualStart == null && race.Track.MaxCars > currentNumberOfCars)
            {

                var carRace = new CarInRace()
                {
                    CarId = carId,
                    RaceId = raceId,
                    Car = car,
                    Race = race,
                };

                _context.CarsInRace.Add(carRace);
                _context.SaveChanges();
                return (true, "YAY! " + carRace.Car.Name + " " + carRace.Race.Track.Name);
            } else if (race.ActualStart != null)
            {
                return (false, "Race started");
            } else
            {
                return (false, "Race is full");
            }
        }

        public int Create(RaceCreateDTO race)
        {
            var entity = new Race
            {
                Id = race.Id,
                TrackId = race.TrackId,
                NumberOfLaps = race.NumberOfLaps,
                PlannedStart = race.PlannedStart,
                ActualStart = race.ActualStart,
                PlannedEnd = race.PlannedEnd,
                ActualEnd = race.PlannedEnd
            };

            _context.Races.Add(entity);
            _context.SaveChanges();

            return 0;
        }

        public (bool ok, string error) Delete(int raceId)
        {
            var race = _context.Races.Find(raceId);

            if (race == null) return (false, "No race with that ID");

            _context.Races.Remove(race);
            _context.SaveChanges();

            return (true, "Race has been deleted");
        }
        
        public IEnumerable<RaceListDTO> Read()
        {
            var race = from c in _context.Races
                       select new RaceListDTO
                       {
                           Id = c.Id,
                           TrackName = c.Track.Name,
                           NumberOfLaps = c.NumberOfLaps,
                           Start = c.ActualStart != null ? c.ActualStart : c.PlannedStart,
                           End = c.ActualEnd != null ? c.ActualEnd : c.PlannedEnd,
                           MaxCars = c.Track.MaxCars,
                           NumberOfCars = _context.CarsInRace.Where(o => o.RaceId == c.Id).Count(),
                           WinningCar = (from r in _context.CarsInRace
                                         where r.EndPosition == 1 && r.Race == c
                                         select r.Car.Name).FirstOrDefault(),
                           WinningDriver = (from r in _context.CarsInRace
                                            where r.EndPosition == 1 && r.Race == c
                                            select r.Car.Driver).FirstOrDefault()
                       };

            return race.ToList();
        }

        public RaceCreateDTO Read(int raceId)
        {
            return (from r in _context.Races
                   where r.Id == raceId
                   select new RaceCreateDTO
                   {
                        Id = r.Id,
                        TrackId = r.TrackId,
                        NumberOfLaps = r.NumberOfLaps,
                        PlannedStart = r.PlannedStart,
                        ActualStart = r.ActualStart,
                        PlannedEnd = r.PlannedEnd,
                        ActualEnd = r.ActualEnd
                   }).FirstOrDefault();
        }

        public (bool ok, string error) RemoveCarFromRace(int carId, int raceId)
        {
            var car = _context.Cars.Find(carId);
            var race = _context.Races.Find(raceId);
            if (car == null) return (false, "Couldn't find car");
            if (race == null) return (false, "Couldn't find race");

            if (race.ActualStart != null)
            {
                return (false, "Race has begun, cars cannot be removed");
            }

            var carRace = new CarInRace()
            {
                CarId = carId,
                RaceId = raceId,
                Car = car,
                Race = race,
            };

            _context.CarsInRace.Add(carRace);
            _context.SaveChanges();

            return (true, "Car has been removed from race");
        }

        public (bool ok, string error) Update(RaceCreateDTO race)
        {
            var raceTrack = _context.Races.Find(race.Id);
            if(raceTrack == null) { return (false, "Race not found"); }
            raceTrack.NumberOfLaps = race.NumberOfLaps;
            raceTrack.PlannedStart = (race.PlannedStart != null) ? race.PlannedStart : raceTrack.PlannedStart;
            raceTrack.ActualStart = (race.ActualStart != null) ? race.ActualStart : raceTrack.ActualStart;
            raceTrack.PlannedEnd = (race.PlannedEnd != null) ? race.PlannedEnd : raceTrack.PlannedEnd;
            raceTrack.ActualEnd = (race.ActualEnd != null) ? race.ActualEnd : raceTrack.ActualEnd;
            if (raceTrack.TrackId != race.TrackId && _context.Tracks.Find(race.TrackId) != null)
            {
                raceTrack.TrackId = race.TrackId;
                raceTrack.Track = _context.Tracks.Find(race.TrackId);
            }
            _context.SaveChanges();
            return (true, "Track is now updated ");
        }

        public (bool ok, string error) UpdateCarInRace(RaceCarDTO car)
        {
            var carInRace = _context.CarsInRace.Find(car.CarId, car.RaceId);
       
            if (carInRace == null) return (false, "Car not found");

            carInRace.CarId = car.CarId;
            carInRace.RaceId = car.RaceId;
            carInRace.StartPosition = car.StartPosition;
            carInRace.EndPosition = car.EndPosition;
            carInRace.FastestLap = car.FastestLap;
            carInRace.TotalTime = car.TotalTime;

            _context.SaveChanges();

            return (true, "Car has been updated");
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
