using LotteryNumbers.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LotteryNumbers.Libs
{
    public class LineGenerator : ILineGenerator
    {
        private Random _random = new Random();

        private Dictionary<ColoursEnum, string> _colourEnumMap = new Dictionary<ColoursEnum, string>
        {
            { ColoursEnum.Grey, "#949c9e" },
            { ColoursEnum.Blue, "#33ccff" },
            { ColoursEnum.Pink, "#ff33cc" },
            { ColoursEnum.Green, "#00ff00" },
            { ColoursEnum.Yellow, "#ffff00" },
            { ColoursEnum.Gold, "#FFD700" },
        };

        public IList<LotteryLine> GenerateNumbers(int numberOfLines, bool bonusBall)
        {
            List<LotteryLine> res = new List<LotteryLine>();

            for (int i = 0; i < numberOfLines; i++)
            {
                res.Add(GenerateLine(6, bonusBall));
            }

            return res;
        }

        private LotteryLine GenerateLine(int numberOfBalls, bool bonusBall)
        {
            LotteryLine line = new LotteryLine
            {
                Id = Guid.NewGuid(),
                Created = DateTime.UtcNow,
                Balls = new List<BallData>()
            };

            for(int i = 0; i < numberOfBalls; i++)
            {
                int number = 0;

                do
                {
                    number = _random.Next(1, 49);
                } 
                while (line.Balls.Any(o => o.Number == number));

                ColoursEnum colour = GetBallColour(number);

                line.Balls.Add(new BallData
                {
                    Number = number,
                    Colour = colour,
                    ColourId = _colourEnumMap[colour]
                });
            }

            if (bonusBall)
            {
                line.Balls.Add(new BallData
                {
                    Number = _random.Next(1, 10),
                    Colour = ColoursEnum.Gold,
                    ColourId = _colourEnumMap[ColoursEnum.Gold],
                    IsBonusBall = true
                });
            }

            line.Balls = line.Balls.OrderBy(o => o.IsBonusBall).ThenBy(o => o.Number).ToList();

            return line;
        }

        private ColoursEnum GetBallColour(int number)
        {
            if (number >= 1 && number <= 9)
            {
                return ColoursEnum.Grey;
            }
            else if (number >= 10 && number <= 19)
            {
                return ColoursEnum.Blue;
            }
            else if (number >= 20 && number <= 29)
            {
                return ColoursEnum.Pink;
            }
            else if (number >= 30 && number <= 39)
            {
                return ColoursEnum.Green;
            }
            else if (number >= 40 && number <= 49)
            {
                return ColoursEnum.Yellow;
            }

            return ColoursEnum.Grey;
        }
    }
}
