using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance
{
    public class DisjointMergeCandleTokenizer : AbstractCandleTokenizer
    {
        AbstractCandleTokenizer first, second;

        public DisjointMergeCandleTokenizer(AbstractCandleTokenizer lhs, AbstractCandleTokenizer rhs)
        {
            first = lhs;
            second = rhs;
        }

        public override Candle this[int index]
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

        public override int GetLength()
        {
            return first.GetLength() + second.GetLength();
        }
    }
}
