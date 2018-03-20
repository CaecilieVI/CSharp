using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BDSA2017.Assignment06.Entities
{
    public interface ISlotCarContext
    {
        DbSet<Car> Cars { get; set; }
        DbSet<CarInRace> CarsInRace { get; set; }
        DbSet<Race> Races { get; set; }
        DbSet<Track> Tracks { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}