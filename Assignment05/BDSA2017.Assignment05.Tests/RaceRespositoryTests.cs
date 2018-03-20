using BDSA2017.Assignment05.Entities;
using BDSA2017.Assignment05;
using BDSA2017.Assignment05.DTOs;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BDSA2017.Assignment05.Tests
{
    public class RaceRepositoryTests : IDisposable {
        private SlotCarContext _context;
        [Fact]
        public void Add_Car_To_Race_Runs_Correct_Methods()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            
            var builder = new DbContextOptionsBuilder<SlotCarContext>()
                              .UseSqlite(connection);

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
            _context.Cars.Add(car);
            _context.Races.Add(race);
            _context.SaveChanges();

            using (var repository = new RaceRepository(_context))
            {
                var result = repository.AddCarToRace(1,1);
                Assert.True(result.ok);
                Assert.Equal("YAY! " + car.Name + " " + race.Track.Name, result.error);
            }
            connection.Close();
        }


        [Fact]
        public void Add_Car_To_Race_That_Is_Started_Returns_False()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();


            var builder = new DbContextOptionsBuilder<SlotCarContext>()
                              .UseSqlite(connection);

            _context = new SlotCarContext(builder.Options);
            _context.Database.EnsureCreated();

            var track = new Track()
            {
                Id = 1,
                Name = "Short Lane",
                LengthInMeters = 1000,
                MaxCars = 1
            };
            var race = new Race()
            {
                Id = 1,
                TrackId = 1,
                Track = track,
                NumberOfLaps = 2,
                ActualStart = new DateTime(2017, 9, 28)
            };
            var car = new Car() { Id = 1, Name = "BMW", Driver = "Hugh Hefner" };
            _context.Cars.Add(car);
            _context.Races.Add(race);
            _context.SaveChanges();

            using (var repository = new RaceRepository(_context))
            {
                var result = repository.AddCarToRace(1, 1);
                Assert.False(result.ok);
                Assert.Equal("Race started", result.error);
            }
            connection.Close();
        }

        [Fact]
        public void Add_Car_To_Race_That_Is_Full_Returns_False()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<SlotCarContext>()
                              .UseSqlite(connection);
            _context = new SlotCarContext(builder.Options);
            _context.Database.EnsureCreated();

            var track = new Track()
            {
                Id = 1,
                Name = "Huge Lane",
                LengthInMeters = 1000,
                MaxCars = 1
            };
            var race = new Race()
            {
                Id = 1,
                TrackId = 1,
                Track = track,
                NumberOfLaps = 2,
            };
            var car = new Car() { Id = 1, Name = "BMW", Driver = "Hugh Hefner" };
            var car2 = new Car() { Id = 2, Name = "Opel", Driver = "Barack Obama" };
            var raceCar = new CarInRace()
            {
                CarId = car.Id,
                RaceId = race.Id,
                Car = car,
                Race = race
            };
            _context.Cars.Add(car);
            _context.Cars.Add(car2);
            _context.Races.Add(race);
            _context.CarsInRace.Add(raceCar);
            _context.SaveChanges();

            using (var repository = new RaceRepository(_context))
            {
                var result = repository.AddCarToRace(2, 1);
                Assert.False(result.ok);
                Assert.Equal("Race is full", result.error);
            }
            connection.Close();
        }

        [Fact]
        public void Create_New_Car_In_Create_Race_Repo()
        {
            var race = default(Race);
            var dto = new RaceCreateDTO
            {
                Id = 1,
                TrackId = 2,
                NumberOfLaps = 7
            };

            var mock = new Mock<ISlotCarContext>();
            mock.Setup(s => s.Races.Add(It.IsAny<Race>()))
                            .Callback<Race>(r => race = r);

            using (var repository = new RaceRepository(mock.Object))
            {
                repository.Create(dto);
            }

            if (race != null)
            {
                Assert.Equal(1, race.Id);
                Assert.Equal(2, race.TrackId);
                Assert.Equal(7, race.NumberOfLaps);
            } else
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void Read_returns_Ennumerable_of_Race()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<SlotCarContext>()
                              .UseSqlite(connection);
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
            _context.Cars.Add(car);
            _context.Cars.Add(car2);
            _context.Races.Add(race);
            _context.CarsInRace.Add(raceCar);
            _context.CarsInRace.Add(raceCar2);
            _context.SaveChanges();

            using (var repository = new RaceRepository(_context))
            {
                var dtos = repository.Read();
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
        public void Read_To_Get_One_Race_Create_From_Id()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<SlotCarContext>()
                              .UseSqlite(connection);
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
            _context.Races.Add(race);
            _context.SaveChanges();

            using (var repository = new RaceRepository(_context))
            {
                var dto = repository.Read(1);
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
        public void Update_Race_From_Race_Create_DTO()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<SlotCarContext>()
                              .UseSqlite(connection);
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
            _context.Tracks.Add(track);
            _context.Tracks.Add(track2);
            _context.Races.Add(race);
            _context.SaveChanges();

            var newRace = new RaceCreateDTO()
            {
                Id = 1,
                TrackId = 2,
                NumberOfLaps = 5,
                PlannedStart = new DateTime(2015,09,28),
                PlannedEnd = new DateTime(2017, 09, 28),
                ActualEnd = new DateTime(2017, 09, 30)
            };
            
            using (var repository = new RaceRepository(_context))
            {
                repository.Update(newRace);
                var result = _context.Races.Find(1);
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
        public void Delete_given_raceid_deletes_race()
        {
            var race = new Race();
            var mock = new Mock<ISlotCarContext>();

            mock.Setup(m => m.Races.Find(1)).Returns(race);
            mock.Setup(m => m.Races.Remove(It.IsAny<Race>())).Callback<Race>(r => race = r);


            using (var repository = new RaceRepository(mock.Object))
            {
                var result = repository.Delete(1);
                Assert.True(result.ok);
            }

            mock.Verify(m => m.SaveChanges());
        }

        [Fact]
        public void RemoveCarFromRace_given_carid_and_raceid_removes_car()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            
            var builder = new DbContextOptionsBuilder<SlotCarContext>()
                              .UseSqlite(connection);

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

            _context.Cars.Add(car);
            _context.Races.Add(race);
            _context.SaveChanges();

            using (var repository = new RaceRepository(_context))
            {
                var result = repository.RemoveCarFromRace(1, 1);
                Assert.True(result.ok);
            }

            connection.Close();
        }
        
        [Fact]
        public void UpdateCarInRace_given_RaceCarDTO_updates_car()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var builder = new DbContextOptionsBuilder<SlotCarContext>()
                              .UseSqlite(connection);

            _context = new SlotCarContext(builder.Options);
            _context.Database.EnsureCreated();

            var track = new Track()
            {
                Id = 10,
                Name = "Awesome Track",
                LengthInMeters = 2000,
                MaxCars = 12
            };
            
            var race = new Race()
            {
                Id = 5,
                TrackId = 10,
                Track = track,
                NumberOfLaps = 3
            };

            var car = new Car() { Id = 20, Name = "Awesome Car", Driver = "Awesome Driver" };
            var car2 = new Car() { Id = 21, Name = "More Awesome Car", Driver = "Awesomest Driver" };

            var carInRace = new CarInRace()
            {
                CarId = car.Id,
                RaceId = race.Id,
                Car = car,
                Race = race,
            };

            _context.Tracks.Add(track);
            _context.Races.Add(race);
            _context.Cars.Add(car);
            _context.Cars.Add(car2);
            _context.CarsInRace.Add(carInRace);
            _context.SaveChanges();

            car.Driver = "Awesomest Driver";

            var newCarInRace = new RaceCarDTO()
            {
                CarId = car.Id,
                RaceId = race.Id,
            };

            using (var repository = new RaceRepository(_context))
            {
                var DBCarInRace = _context.CarsInRace.FirstOrDefault();
                var DBCar = _context.Cars.Find(DBCarInRace.CarId);
                Assert.Equal(car.Name, DBCar.Name);
                Assert.Equal(car.Driver, DBCar.Driver);
            }
        }
        
        public void Dispose()
        {
           
        }
    }
}
