using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BDSA2017.Assignment04
{
    public partial class CarInRace
    {
        [Column("raceID")]
        public int RaceId { get; set; }
        [Column("carID")]
        public int CarId { get; set; }
        [Column("best_lap")]
        public int BestLap { get; set; }
        [Column("end_position")]
        public int? EndPosition { get; set; }
        [Column("start_position")]
        public int StartPosition { get; set; }
        [Column("total_race_time")]
        public int TotalRaceTime { get; set; }

        [ForeignKey("CarId")]
        [InverseProperty("CarInRace")]
        public Car Car { get; set; }
        [ForeignKey("RaceId")]
        [InverseProperty("CarInRace")]
        public Race Race { get; set; }
    }
}
