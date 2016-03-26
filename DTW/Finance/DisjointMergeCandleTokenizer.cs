using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance
{
    public class DisjointMergeCandleTokenizer : ICandleTokenizer
    {
        ICandleTokenizer first, second;

        public DisjointMergeCandleTokenizer(ICandleTokenizer lhs, ICandleTokenizer rhs)
        {
            first = lhs;
            second = rhs;
        }

        public Candle this[int index]
        {
            get
            {
                if (index < first.GetLength())
                {
                    return first[index];
                }
                return second[index - first.GetLength()];
            }
        }

        public int GetLength()
        {
            return first.GetLength() + second.GetLength();
        }
    }
}
