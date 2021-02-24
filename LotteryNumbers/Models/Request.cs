using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LotteryNumbers.Models
{
    public class Request
    {
        public int NumberOfLines { get; set; }

        public bool BonusBall { get; set; }
    }
}
