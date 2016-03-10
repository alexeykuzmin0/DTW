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
            pattern = new List<T>(pattern);
            dp = new List<List<double>>();
            dp.Add(new List<double>(pattern.Count() + 1));
            dp[0].Add(0.0);
            dp[0].AddRange(Enumerable.Repeat(double.PositiveInfinity, pattern.Count()));
        }

        public double GetResult()
        {
            return dp.Last().Last();
        }
    }
}
