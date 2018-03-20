using BDSA2017.Assignment06.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using BDSA2017.Assignment06.DTOs;
using System.Threading.Tasks;
using BDSA2017.Assignment06.Entities;

namespace BDSA2017.Assignment06
{
    public class RaceRepository : IRaceRepository
    {
        private readonly ISlotCarContext _context;

        public RaceRepository(ISlotCarContext context)
        {
            _context = context;
        }

        public async Task<(bool ok, string error)> AddCarToRaceAsync(int carId, int raceId, int? startPosition = null)
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

                await _context.CarsInRace.AddAsync(carRace);
                await _context.SaveChangesAsync();

                return (true, "Car has been added");
            }
            else if (race.ActualStart != null)
            {
                return (false, "Race started");
            }
            else
            {
                return (false, "Race is full");
            }
        }

        public async Task<int> CreateAsync(RaceCreateDTO race)
        {
            var entity = new Race
            {
                TrackId = race.TrackId,
                NumberOfLaps = race.NumberOfLaps,
                PlannedStart = race.PlannedStart,
                ActualStart = race.ActualStart,
                PlannedEnd = race.PlannedEnd,
                ActualEnd = race.PlannedEnd
            };

            await _context.Races.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<(bool ok, string error)> DeleteAsync(int raceId)
        {
            var race = await _context.Races.FindAsync(raceId);

            if (race == null) return (false, "No race with that ID");

            _context.Races.Remove(race);
            await _context.SaveChangesAsync();

            return (true, "Race has been deleted");
        }

        public async Task<IEnumerable<RaceListDTO>> ReadAsync()
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

        public async Task<RaceCreateDTO> ReadAsync(int raceId)
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

        public async Task<(bool ok, string error)> RemoveCarFromRaceAsync(int carId, int raceId)
        {
            var car = await _context.Cars.FindAsync(carId);
            var race = await _context.Races.FindAsync(raceId);
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

            await _context.CarsInRace.AddAsync(carRace);
            await _context.SaveChangesAsync();

            return (true, "Car has been removed from race");
        }

        public async Task<(bool ok, string error)> UpdateAsync(RaceCreateDTO race)
        {
            var raceTrack = await _context.Races.FindAsync(race.Id);
            if (raceTrack == null) { return (false, "Race not found"); }
            raceTrack.NumberOfLaps = race.NumberOfLaps;
            raceTrack.PlannedStart = (race.PlannedStart != null) ? race.PlannedStart : raceTrack.PlannedStart;
            raceTrack.ActualStart = (race.ActualStart != null) ? race.ActualStart : raceTrack.ActualStart;
            raceTrack.PlannedEnd = (race.PlannedEnd != null) ? race.PlannedEnd : raceTrack.PlannedEnd;
            raceTrack.ActualEnd = (race.ActualEnd != null) ? race.ActualEnd : raceTrack.ActualEnd;
            if (raceTrack.TrackId != race.TrackId && _context.Tracks.Find(race.TrackId) != null)
            {
                raceTrack.TrackId = race.TrackId;
                raceTrack.Track = await _context.Tracks.FindAsync(race.TrackId);
            }
            await _context.SaveChangesAsync();
            return (true, "Track is now updated ");
        }

        public async Task<(bool ok, string error)> UpdateCarInRaceAsync(RaceCarDTO car)
        {
            var carInRace = await _context.CarsInRace.FindAsync(car.CarId, car.RaceId);

            if (carInRace == null) return (false, "Car not found");

            carInRace.CarId = car.CarId;
            carInRace.RaceId = car.RaceId;
            carInRace.StartPosition = car.StartPosition;
            carInRace.EndPosition = car.EndPosition;
            carInRace.FastestLap = car.FastestLap;
            carInRace.TotalTime = car.TotalTime;

            await _context.SaveChangesAsync();

            return (true, "Car has been updated");
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~RaceRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
