using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIComponents
{
    public class CandleStickChart : ZedGraph.ZedGraphControl
    {
        public CandleStickChart()
        {
            GraphPane.Title.Text = "No data loaded";
            GraphPane.XAxis.Title.Text = "";
            GraphPane.YAxis.Title.Text = "";
        }
    }
}
