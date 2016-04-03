using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance
{
    public class PartialCandleTokenizer : AbstractCandleTokenizer
    {
        private AbstractCandleTokenizer ct;
        private int start;
        private int length;

        public PartialCandleTokenizer(AbstractCandleTokenizer ct, int start, int length)
        {
            this.ct = ct;
            this.start = start;
            this.length = length;
        }

        public override Candle this[int index]
        {
            get
            {
                return ct[index + start];
            }
        }

        public override int GetLength()
        {
            return length;
        }
    }
}
