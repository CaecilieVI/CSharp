using System;

namespace BDSA2017.Assignment04
{
    public class TrackInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfRaces { get; set; }
        public string FastestsCar { get; set; }
        public string FastestsDriver { get; set; }
        public TimeSpan FastestLap { get; set; }

        public override bool Equals(object other)
        {
            var toCompareWith = other as TrackInfo;
            if (toCompareWith == null)
                return false;
            return this.Name == toCompareWith.Name &&
                this.FastestLap == toCompareWith.FastestLap &&
                this.FastestsCar == toCompareWith.FastestsCar &&
                this.FastestsDriver == toCompareWith.FastestsDriver &&
                this.Id == toCompareWith.Id &&
                this.NumberOfRaces == toCompareWith.NumberOfRaces;
        }
    }
}
