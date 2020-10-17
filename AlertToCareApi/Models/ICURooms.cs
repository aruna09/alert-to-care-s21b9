using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlertToCareApi.Models
{
    public class ICURooms
    {
        [Key]
        public int icuRoomNo { get; set; }
        public string capacityLevel { get; set; }
        public int floorNo { get; set; }
    }
}
