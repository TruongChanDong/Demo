using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class TestData
    {
        public DateTime timestamp { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }

        public override string? ToString()
        {
            return "Timestamp: " + timestamp +" "
                +"x:"+x + " y:" + y +" z:" + z;
        }
    }
}
