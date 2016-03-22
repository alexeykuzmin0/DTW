using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class DTW<T> where T : IDistansable
    {
        List<T> pattern;
        List<List<double>> dp;

        public DTW(IEnumerable<T> pattern)
        {
            this.pattern = new List<T>(pattern);
            dp = new List<List<double>>();
            dp.Add(new List<double>(pattern.Count() + 1));
            dp[0].Add(0.0);
            dp[0].AddRange(Enumerable.Repeat(double.PositiveInfinity, pattern.Count()));
        }

        public void Process(T input)
        {
            dp.Add(new List<double>(pattern.Count() + 1));
            dp.Last().Add(0.0);
            for (int i = 0; i < pattern.Count(); ++i)
            {
                dp.Last().Add(Math.Min(dp[dp.Count() - 2][i + 1],
                    Math.Min(dp[dp.Count() - 2][i], dp.Last()[i])) + pattern[i].DistanceTo(input));
            }
        }

        public double GetResult()
        {
            return dp.Last().Last();
        }
    }
}
