using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BDSA2017.Assignment04
{
    public partial class Race
    {
        public Race()
        {
            CarInRace = new HashSet<CarInRace>();
        }

        [Column("raceID")]
        public int RaceId { get; set; }
        [Column("actual_end_time", TypeName = "datetime")]
        public DateTime? ActualEndTime { get; set; }
        [Column("actual_start_time", TypeName = "datetime")]
        public DateTime? ActualStartTime { get; set; }
        [Column("number_of_laps")]
        public int NumberOfLaps { get; set; }
        [Column("planned_end_time", TypeName = "datetime")]
        public DateTime? PlannedEndTime { get; set; }
        [Column("planned_start_time", TypeName = "datetime")]
        public DateTime? PlannedStartTime { get; set; }
        [Column("trackID")]
        public int TrackId { get; set; }

        [ForeignKey("TrackId")]
        [InverseProperty("Race")]
        public Track Track { get; set; }
        [InverseProperty("Race")]
        public ICollection<CarInRace> CarInRace { get; set; }
    }
}
