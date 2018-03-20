﻿// <auto-generated />
using BDSA2017.Assignment07.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace BDSA2017.Assignment07.Entities.Migrations
{
    [DbContext(typeof(SlotCarContext))]
    [Migration("20171012194421_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BDSA2017.Assignment07.Entities.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Driver")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("BDSA2017.Assignment07.Entities.CarInRace", b =>
                {
                    b.Property<int>("CarId");

                    b.Property<int>("RaceId");

                    b.Property<int?>("EndPosition");

                    b.Property<long?>("FastestLap");

                    b.Property<int?>("StartPosition");

                    b.Property<long?>("TotalTime");

                    b.HasKey("CarId", "RaceId");

                    b.HasIndex("RaceId");

                    b.ToTable("CarsInRace");
                });

            modelBuilder.Entity("BDSA2017.Assignment07.Entities.Race", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("ActualEnd");

                    b.Property<DateTime?>("ActualStart");

                    b.Property<int>("NumberOfLaps");

                    b.Property<DateTime?>("PlannedEnd");

                    b.Property<DateTime?>("PlannedStart");

                    b.Property<int>("TrackId");

                    b.HasKey("Id");

                    b.HasIndex("TrackId");

                    b.ToTable("Races");
                });

            modelBuilder.Entity("BDSA2017.Assignment07.Entities.Track", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("BestLapInTicks");

                    b.Property<double>("LengthInMeters");

                    b.Property<int>("MaxCars");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("BDSA2017.Assignment07.Entities.CarInRace", b =>
                {
                    b.HasOne("BDSA2017.Assignment07.Entities.Car", "Car")
                        .WithMany("CarsInRace")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BDSA2017.Assignment07.Entities.Race", "Race")
                        .WithMany("CarsInRace")
                        .HasForeignKey("RaceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BDSA2017.Assignment07.Entities.Race", b =>
                {
                    b.HasOne("BDSA2017.Assignment07.Entities.Track", "Track")
                        .WithMany("Races")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
