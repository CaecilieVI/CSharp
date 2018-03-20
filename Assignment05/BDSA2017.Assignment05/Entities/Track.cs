using System.ComponentModel.DataAnnotations;

namespace BDSA2017.Assignment05.Entities
{
    public class Track
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public double LengthInMeters { get; set; }

        public long? BestTime { get; set; }

        public int MaxCars { get; set; }
    }
}