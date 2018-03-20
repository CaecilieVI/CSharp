using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BDSA2017.Assignment04
{
    public partial class Slot_Car_TournamentContext : DbContext
    {
        public virtual DbSet<Car> Car { get; set; }
        public virtual DbSet<CarInRace> CarInRace { get; set; }
        public virtual DbSet<Race> Race { get; set; }
        public virtual DbSet<Track> Track { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Slot_Car_Tournament;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.Property(e => e.CarId).ValueGeneratedNever();

                entity.Property(e => e.DriverName).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<CarInRace>(entity =>
            {
                entity.HasKey(e => new { e.RaceId, e.CarId });

                entity.HasIndex(e => e.CarId);

                entity.HasIndex(e => e.RaceId)
                    .HasName("IX_CarInRace");

                entity.Property(e => e.BestLap).IsUnicode(false);

                entity.Property(e => e.TotalRaceTime).IsUnicode(false);

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.CarInRace)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CarInRace_Car");

                entity.HasOne(d => d.Race)
                    .WithMany(p => p.CarInRace)
                    .HasForeignKey(d => d.RaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CarInRace_Race");
            });

            modelBuilder.Entity<Race>(entity =>
            {
                entity.HasIndex(e => e.TrackId)
                    .HasName("IX_Race_track_name");

                entity.Property(e => e.RaceId).ValueGeneratedNever();

                entity.HasOne(d => d.Track)
                    .WithMany(p => p.Race)
                    .HasForeignKey(d => d.TrackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Race_Track");
            });

            modelBuilder.Entity<Track>(entity =>
            {
                entity.Property(e => e.TrackId).ValueGeneratedNever();

                entity.Property(e => e.BestTime).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);
            });
        }
    }
}
