using Microsoft.EntityFrameworkCore;

namespace BDSA2017.Assignment07.Entities
{
    public class SlotCarContext : DbContext, ISlotCarContext
    {
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<CarInRace> CarsInRace { get; set; }

        public SlotCarContext(DbContextOptions<SlotCarContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=SlotCarTournament;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarInRace>().HasKey(c => new { c.CarId, c.RaceId });
        }
    }
}
