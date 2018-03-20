using System;
using System.Collections.Generic;

namespace BDSA2017.Assignment04
{
    public class RaceInfo
    {
        public int RaceId { get; set; }
        public int TrackId { get; set; }
        public string TrackName { get; set; }
        public int NumberOfLaps { get; set; }
        public IEnumerable<CarInfo> Cars { get; set; }
        public DateTime? PlannedStart { get; set; }
        public DateTime? ActualStart { get; set; }
        public DateTime? PlannedEnd { get; set; }
        public DateTime? ActualEnd { get; set; }

        public override bool Equals(object other)
        {
            var toCompareWith = other as RaceInfo;
            if (toCompareWith == null)
                return false;
            return 
                this.TrackName == toCompareWith.TrackName &&
                this.TrackId == toCompareWith.TrackId &&
                this.RaceId == toCompareWith.RaceId &&
                this.PlannedStart == toCompareWith.PlannedStart &&
                this.PlannedEnd == toCompareWith.PlannedEnd &&
                this.NumberOfLaps == toCompareWith.NumberOfLaps &&
                this.ActualEnd == toCompareWith.ActualEnd &&
                this.ActualStart == toCompareWith.ActualStart &&
                this.Cars == toCompareWith.Cars;
        }
    }
}
