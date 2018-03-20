using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BDSA2017.Assignment04
{
    public partial class Track
    {
        public Track()
        {
            Race = new HashSet<Race>();
        }

        [Required]
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }
        [Column("best_time")]
        public int BestTime { get; set; }
        [Column("length_meter")]
        public int LengthMeter { get; set; }
        [Column("max_cars")]
        public int MaxCars { get; set; }
        [Column("trackID")]
        public int TrackId { get; set; }

        [InverseProperty("Track")]
        public ICollection<Race> Race { get; set; }
    }
}
