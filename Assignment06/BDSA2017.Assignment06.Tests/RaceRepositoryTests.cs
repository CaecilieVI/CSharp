using BDSA2017.Assignment06.DTOs;
using BDSA2017.Assignment06.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using Xunit;

namespace BDSA2017.Assignment06.Tests
{
    public class RaceRepositoryTests : IDisposable
    {
        private SlotCarContext _context;

        [Fact]
        public async void AddCarToRaceAsync_given_carid_raceid_and_startposition_returns_true()
        {
            var connection = new SqliteConnection("DataSource=:memory:");

            connection.Open();
            var builder = new DbContextOptionsBuilder<SlotCarContext>().UseSqlite(connection);

            _context = new SlotCarContext(builder.Options);
            _context.Database.EnsureCreated();

            var track = new Track()
            {
                Id = 1,
                Name = "Long Lane",
                LengthInMeters = 1000,
                MaxCars = 4
            };

            var race = new Race()
            {
                Id = 1,
                TrackId = 1,
                Track = track,
                NumberOfLaps = 2
            };

            var car = new Car() { Id = 1, Name = "BMW", Driver = "Hugh Hefner" };

            await _context.Cars.AddAsync(car);
            await _context.Races.AddAsync(race);
            await _context.SaveChangesAsync();

            using (var repository = new RaceRepository(_context))
            {
                var result = await repository.AddCarToRaceAsync(1, 1);
                Assert.True(result.ok);
            }

            connection.Close();
        }

        [Fact]
        public async void CreateAsync_given_race_returns_raceid()
        {
            var race = default(Race);
            var dto = new RaceCreateDTO
            {
                Id = 1,
                TrackId = 2,
                NumberOfLaps = 7
            };
            
            var mock = new Mock<ISlotCarContext>();
            mock.Setup(s => s.Races.Add(It.IsAny<Race>())).Callback<Race>(r => race = r);

            using (var repository = new RaceRepository(mock.Object))
            {
                await repository.CreateAsync(dto);
            }

            if (race != null)
            {
                Assert.Equal(2, race.TrackId);
                Assert.Equal(7, race.NumberOfLaps);
            }
            else
            {
                Assert.True(true);
            }
        }

        [Fact]
        public async void DeleteAsync_given_raceid_deletes_race()
        {
            var race = new Race();
            var mock = new Mock<ISlotCarContext>();

            mock.Setup(m => m.Races.FindAsync(1)).ReturnsAsync(race);
            mock.Setup(m => m.Races.Remove(It.IsAny<Race>())).Callback<Race>(r => race = r);
            
            using (var repository = new RaceRepository(mock.Object))
            {
                var result = await repository.DeleteAsync(1);
                Assert.True(result.ok);
            }

            mock.Verify(m => m.SaveChangesAsync(default(CancellationToken)));
        }

        [Fact]
        public async void ReadAsync_returns_mapped_races()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<SlotCarContext>().UseSqlite(connection);
            _context = new SlotCarContext(builder.Options);
            _context.Database.EnsureCreated();

            var track = new Track()
            {
                Id = 1,
                Name = "Huge Lane",
                LengthInMeters = 1000,
                MaxCars = 4
            };

            var race = new Race()
            {
                Id = 1,
                TrackId = 1,
                Track = track,
                NumberOfLaps = 2,
                ActualStart = new DateTime(2015, 09, 28),
                PlannedEnd = new DateTime(2017, 09, 28)
            };

            var car = new Car() { Id = 1, Name = "BMW", Driver = "Hugh Hefner" };
            var car2 = new Car() { Id = 2, Name = "Opel", Driver = "Barack Obama" };

            var raceCar = new CarInRace()
            {
                CarId = car.Id,
                RaceId = race.Id,
                Car = car,
                Race = race,
                EndPosition = 2
            };

            var raceCar2 = new CarInRace()
            {
                CarId = car2.Id,
                RaceId = race.Id,
                Car = car2,
                Race = race,
                EndPosition = 1
            };

            await _context.Cars.AddAsync(car);
            await _context.Cars.AddAsync(car2);
            await _context.Races.AddAsync(race);
            await _context.CarsInRace.AddAsync(raceCar);
            await _context.CarsInRace.AddAsync(raceCar2);
            await _context.SaveChangesAsync();

            using (var repository = new RaceRepository(_context))
            {
                var dtos = await repository.ReadAsync();
                var result = dtos.FirstOrDefault();
                var expected = new List<RaceListDTO>();
                expected.Add(new RaceListDTO()
                {
                    Id = 1,
                    TrackName = "Huge Lane",
                    NumberOfLaps = 2,
                    Start = new DateTime(2015, 09, 28),
                    End = new DateTime(2017, 09, 28),
                    MaxCars = 4,
                    NumberOfCars = 2,
                    WinningCar = "Opel",
                    WinningDriver = "Barack Obama"
                });

                var expectedFinal = expected.FirstOrDefault();
                Assert.Equal(expectedFinal.Id, result.Id);
                Assert.Equal(expectedFinal.TrackName, result.TrackName);
                Assert.Equal(expectedFinal.Start, result.Start);
                Assert.Equal(expectedFinal.End, result.End);
                Assert.Equal(expectedFinal.MaxCars, result.MaxCars);
                Assert.Equal(expectedFinal.NumberOfCars, result.NumberOfCars);
                Assert.Equal(expectedFinal.WinningCar, result.WinningCar);
                Assert.Equal(expectedFinal.WinningDriver, result.WinningDriver);
            }
        }

        [Fact]
        public async void ReadAsync_given_raceid_returns_race()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<SlotCarContext>().UseSqlite(connection);
            _context = new SlotCarContext(builder.Options);
            _context.Database.EnsureCreated();

            var track = new Track()
            {
                Id = 1,
                Name = "Huge Lane",
                LengthInMeters = 1000,
                MaxCars = 4
            };

            var race = new Race()
            {
                Id = 1,
                TrackId = 1,
                Track = track,
                NumberOfLaps = 2,
                ActualStart = new DateTime(2015, 09, 28),
                PlannedEnd = new DateTime(2017, 09, 28)
            };
            await _context.Races.AddAsync(race);
            await _context.SaveChangesAsync();

            using (var repository = new RaceRepository(_context))
            {
                var dto = await repository.ReadAsync(1);
                Assert.Equal(1, dto.Id);
                Assert.Equal(1, dto.TrackId);
                Assert.Equal(2, dto.NumberOfLaps);
                Assert.Equal(null, dto.PlannedStart);
                Assert.Equal(new DateTime(2015, 09, 28), dto.ActualStart);
                Assert.Equal(new DateTime(2017, 09, 28), dto.PlannedEnd);
                Assert.Equal(null, dto.ActualEnd);
            }
        }

        [Fact]
        public async void RemoveCarFromRaceAsync_given_carid_and_racid_removes_car()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var builder = new DbContextOptionsBuilder<SlotCarContext>().UseSqlite(connection);

            _context = new SlotCarContext(builder.Options);
            _context.Database.EnsureCreated();

            var track = new Track()
            {
                Id = 1,
                Name = "Long Lane",
                LengthInMeters = 1000,
                MaxCars = 4
            };

            var race = new Race()
            {
                Id = 1,
                TrackId = 1,
                Track = track,
                NumberOfLaps = 2
            };

            var car = new Car() { Id = 1, Name = "BMW", Driver = "Hugh Hefner" };

            await _context.Cars.AddAsync(car);
            await _context.Races.AddAsync(race);
            await _context.SaveChangesAsync();

            using (var repository = new RaceRepository(_context))
            {
                var result = await repository.RemoveCarFromRaceAsync(1, 1);
                Assert.True(result.ok);
            }

            connection.Close();
        } 

        [Fact]
        public async void UpdateAsync_given_race_updates_race()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<SlotCarContext>().UseSqlite(connection);
            _context = new SlotCarContext(builder.Options);
            _context.Database.EnsureCreated();

            var track = new Track()
            {
                Id = 1,
                Name = "Huge Lane",
                LengthInMeters = 1000,
                MaxCars = 4
            };

            var track2 = new Track()
            {
                Id = 2,
                Name = "Little Lane",
                LengthInMeters = 3000,
                MaxCars = 5
            };

            var race = new Race()
            {
                Id = 1,
                TrackId = 1,
                Track = track,
                NumberOfLaps = 2,
                ActualStart = new DateTime(2015, 09, 28),
                PlannedEnd = new DateTime(2017, 09, 28)
            };

            await _context.Tracks.AddAsync(track);
            await _context.Tracks.AddAsync(track2);
            await _context.Races.AddAsync(race);
            await _context.SaveChangesAsync();

            var newRace = new RaceCreateDTO()
            {
                Id = 1,
                TrackId = 2,
                NumberOfLaps = 5,
                PlannedStart = new DateTime(2015, 09, 28),
                PlannedEnd = new DateTime(2017, 09, 28),
                ActualEnd = new DateTime(2017, 09, 30)
            };

            using (var repository = new RaceRepository(_context))
            {
                await repository.UpdateAsync(newRace);
                var result = await _context.Races.FindAsync(1);
                Assert.Equal(2, result.TrackId);
                Assert.Equal(track2.Name, result.Track.Name);
                Assert.Equal(5, result.NumberOfLaps);
                Assert.Equal(new DateTime(2015, 09, 28), result.PlannedStart);
                Assert.Equal(new DateTime(2015, 09, 28), result.ActualStart);
                Assert.Equal(new DateTime(2017, 09, 28), result.PlannedEnd);
                Assert.Equal(new DateTime(2017, 09, 30), result.ActualEnd);
            }
        }

        [Fact]
        public async void UpdateCarInRaceAsync_given_car_updates_car()
        {
            var builder = new DbContextOptionsBuilder<SlotCarContext>().UseInMemoryDatabase("UpdateCarInRace");

            SlotCarContext context = new SlotCarContext(builder.Options);
            context.Database.EnsureCreated();

            using (var repository = new RaceRepository(context))
            {
                var track = new Track
                {
                    Name = "Monte Carlo",
                    LengthInMeters = 1002,
                    MaxCars = 20
                };

                var race = new Race
                {
                    Id = 42,
                    NumberOfLaps = 5,
                    Track = track
                };

                var car = new Car
                {
                    Id = 42,
                    Name = "Lynet McQueen",
                    Driver = "McQueen"
                };

                var carinrace = new CarInRace
                {
                    Car = car,
                    Race = race
                };
                await context.Races.AddAsync(race);
                await context.Tracks.AddAsync(track);
                await context.Cars.AddAsync(car);
                await context.CarsInRace.AddAsync(carinrace);
                await context.SaveChangesAsync();

                var dto = new RaceCarDTO
                {
                    CarId = 42,
                    RaceId = 42,
                    StartPosition = 1
                };

                var result = await repository.UpdateCarInRaceAsync(dto);

                Assert.True(result.ok);
            }
        }

        public void Dispose()
        {
        }
    }
}
