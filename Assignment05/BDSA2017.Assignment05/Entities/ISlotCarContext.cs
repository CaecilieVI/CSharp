using Microsoft.EntityFrameworkCore;
using System;

namespace BDSA2017.Assignment05.Entities
{
    public interface ISlotCarContext : IDisposable
    {
        DbSet<Car> Cars { get; set; }
        DbSet<CarInRace> CarsInRace { get; set; }
        DbSet<Race> Races { get; set; }
        DbSet<Track> Tracks { get; set; }
        int SaveChanges();
        void Dispose();
    }
}