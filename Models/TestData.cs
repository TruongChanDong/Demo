using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models
{
    internal class TestData
    {
        public DateTime timestamp { get; set; }
        public string x { get; set; }
        public string y { get; set; }
        public string z { get; set; }

        public override string? ToString()
        {
            return "Timestamp: " + timestamp +" "
                +"x:"+x + " y:" + y +" z:" + z;
        }
    }
}
