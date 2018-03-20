using Xunit;
using Moq;
using BDSA2017.Assignment07.Models;
using BDSA2017.Assignment07.Web.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using BDSA2017.Assignment07.Entities;
using System.Collections.Generic;

namespace BDSA2017.Assignment07.Web.Tests
{
    public class TracksControllerTests
    {
        SlotCarContext context;
        public TracksControllerTests()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var builder = new DbContextOptionsBuilder<SlotCarContext>().UseSqlite(connection);

            context = new SlotCarContext(builder.Options);
            context.Database.EnsureCreated();
        }

        [Fact]
        public async Task Get_given_id_returns_track()
        {
            var expected = new TrackDTO { Id = 5, Name = "Awesome Track" };

            var mock = new Mock<ITrackRepository>();
            mock.Setup(m => m.Find(5)).ReturnsAsync(expected);

            var controller = new TracksController(mock.Object);
            
            var actual = await controller.Get(5) as OkObjectResult;

            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public async Task Create_given_track_creates_track()
        {
            var expected = new TrackCreateDTO { Name = "Awesome Track" };

            var mock = new Mock<ITrackRepository>();
            mock.Setup(m => m.Create(expected)).ReturnsAsync(5);

            var controller = new TracksController(mock.Object);

            var actual = await controller.Post(expected) as CreatedAtActionResult;

            Assert.Equal("Get", actual.ActionName);
            Assert.Equal(5, actual.RouteValues["id"]);
        }

        [Fact]
        public async Task Update_given_id_updates_track()
        {           
            var track = new TrackCreateDTO() { Name = "Awesome Track" };
            var updatedTrack = new TrackUpdateDTO() { Name = "Awesome Track", MaxCars = 12, LengthInMeters = 5000 };
            
            var repo = new TrackRepository(context);
            await repo.Create(track);

            var controller = new TracksController(repo);
            var put = await controller.Put(1, updatedTrack) as OkObjectResult;

            var actual = await repo.Find(1);

            Assert.Equal(updatedTrack.MaxCars, actual.MaxCars);
            Assert.Equal(updatedTrack.LengthInMeters, actual.LengthInMeters);
        }

        [Fact]
        public async Task Delete_given_id_deletes_track()
        {
            var track = new TrackCreateDTO() { Name = "Awesome Track", MaxCars = 12, LengthInMeters = 5000 };

            var repo = new TrackRepository(context);
            await repo.Create(track);

            var controller = new TracksController(repo);

            var response = await controller.Delete(1);

            Assert.IsType<OkResult>(response);
            Assert.Null(context.Tracks.Find(1));
        }

        [Fact]
        public async Task Delete_given_non_exiting_id()
        {
            var track = new TrackCreateDTO() { Name = "Awesome Track", MaxCars = 12, LengthInMeters = 5000 };

            var repo = new TrackRepository(context);
            await repo.Create(track);

            var controller = new TracksController(repo);

            var response = await controller.Delete(2);

            Assert.IsType<NotFoundResult>(response);
            Assert.Null(context.Tracks.Find(2));
        }

        private void Dispose()
        {
            context.Dispose();
        }
    }
}
