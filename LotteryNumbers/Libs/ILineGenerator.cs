using LotteryNumbers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LotteryNumbers.Libs
{
    public interface ILineGenerator
    {
        public IList<LotteryLine> GenerateNumbers(int numberOfLines, bool bonusBall);
    }
}
