using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AlertToCareApi.Models
{
    public class Patients
    {
        [Key]
        public int patientId { get; set; }
        public string patientName { get; set; }
        public int patientAge { get; set; }
        public string contactNo { get; set; }
        [ForeignKey("bedId")]
        public int bedId { get; set; }
    }
}
