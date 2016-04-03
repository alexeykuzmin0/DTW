using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance
{
    public class PartialCandleTokenizer : AbstractCandleTokenizer
    {
        public override Candle this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int GetLength()
        {
            throw new NotImplementedException();
        }
    }
}
