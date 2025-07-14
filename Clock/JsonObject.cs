using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clock
{
    public class JsonObject
    {
        public class WidgetConfig
        {
            public class Rootobject
            {
                public string TimeFormat { get; set; }
                public int Refresh { get; set; }
                public int FontSize { get; set; }
                public int Opacity { get; set; }
            }
        }
    }
}
