using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlertToCareApi.Utilities
{
    public class Alarm
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public List<string> Messages { get; set; }

    }
}
