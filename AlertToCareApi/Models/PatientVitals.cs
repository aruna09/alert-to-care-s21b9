using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AlertToCareApi.Models
{
    public class PatientVitals
    {
        [Key]
        public int vitalsInfoId { get; set; }
        [ForeignKey("patientId")]
        public int patientId { get; set; }
        public double bpmRate { get; set; }
        public double spo2Rate { get; set; }
        public double respRate { get; set; }
    }
}
