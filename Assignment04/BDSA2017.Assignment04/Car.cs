using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BDSA2017.Assignment04
{
    public partial class Car
    {
        public Car()
        {
            CarInRace = new HashSet<CarInRace>();
        }

        [Column("carID")]
        public int CarId { get; set; }
        [Required]
        [Column("driver_name")]
        [StringLength(50)]
        public string DriverName { get; set; }
        [Required]
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("Car")]
        public ICollection<CarInRace> CarInRace { get; set; }
    }
}
