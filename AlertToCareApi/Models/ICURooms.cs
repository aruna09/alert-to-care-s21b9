using System.ComponentModel.DataAnnotations;

namespace AlertToCareApi.Models
{
    public class IcuRooms
    {
        [Key]
        public int IcuRoomNo { get; set; }
        public string CapacityLevel { get; set; }
        public int FloorNo { get; set; }
    }
}
