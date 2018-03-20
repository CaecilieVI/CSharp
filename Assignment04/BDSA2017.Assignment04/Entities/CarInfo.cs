using System;

namespace BDSA2017.Assignment04
{
    public class CarInfo
    {
        public int CarId { get; set; }
        public string CarName { get; set; }
        public string DriverName { get; set; }
        public int StartPosition { get; set; }
        public int? EndPosition { get; set; }
        public long? BestLapInTicks { get; set; }
        public long? TotalRaceTimeInTicks { get; set; }

        //These next two methods are probably not implemented correctly...?
        public TimeSpan? BestLap => BestLapInTicks.HasValue ? TimeSpan.FromTicks(BestLapInTicks.Value) : default(TimeSpan?);
        public TimeSpan? TotalRaceTime => TotalRaceTimeInTicks.HasValue ? TimeSpan.FromTicks(TotalRaceTimeInTicks.Value) : default(TimeSpan?);

        public override bool Equals(object other)
        {
            var toCompareWith = other as CarInfo;
            if (toCompareWith == null)
                return false;
            return
                this.TotalRaceTimeInTicks == toCompareWith.TotalRaceTimeInTicks &&
                this.BestLapInTicks == toCompareWith.BestLapInTicks &&
                this.CarId == toCompareWith.CarId &&
                this.CarName == toCompareWith.CarName &&
                this.DriverName == toCompareWith.DriverName &&
                this.EndPosition == toCompareWith.EndPosition &&
                this.StartPosition == toCompareWith.StartPosition &&
                this.BestLap == toCompareWith.BestLap &&
                this.TotalRaceTime == toCompareWith.TotalRaceTime;

        }
    }
}
