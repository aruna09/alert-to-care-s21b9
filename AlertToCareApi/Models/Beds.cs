using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AlertToCareApi.Models
{
    public class Beds
    {
        [Key]
        public int BedId { get; set; }
        public int icuRoomNo{ get; set; }
        [ForeignKey("IcuRoomNo")]
        public bool OccupancyStatus { get; set; }
        public int LayoutId { get; set; }

    }
}
