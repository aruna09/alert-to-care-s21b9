using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlertToCareApi.Models
{
    public class Beds
    {
        [Key]
        public int BedId { get; set; }
        public int IcuRoomNo{ get; set; }
        [ForeignKey("IcuRoomNo")]
        public bool OccupancyStatus { get; set; }
        public int LayoutId { get; set; }

    }
}
