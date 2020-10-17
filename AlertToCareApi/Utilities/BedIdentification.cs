using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace AlertToCareApi.Utilities
{
    public class BedIdentification
    {
        public int BedId { get; set; }
        public int icuRoomNo { get; set; }
        public bool OccupancyStatus { get; set; }
        public int BedSerialNo { get; set; }
    }
}
