using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlertToCareApi.Models
{
    public class Patients
    {
        [Key]
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int PatientAge { get; set; }
        public string ContactNo { get; set; }
        [ForeignKey("bedId")]
        public int BedId { get; set; }
    }
}
