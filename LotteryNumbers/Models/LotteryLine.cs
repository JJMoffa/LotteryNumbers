using LotteryNumbers.Libs;
using System;
using System.Collections.Generic;

namespace LotteryNumbers.Models
{
    [Serializable]
    public class LotteryLine
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public IList<BallData> Balls { get; set; }
    }

    [Serializable]
    public class BallData
    {
        public int Number { get; set; }

        public ColoursEnum Colour { get; set; }

        public string ColourId { get; set; }

        public bool IsBonusBall { get; set; }
    }
}
