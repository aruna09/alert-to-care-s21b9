using System;
using System.Collections.Generic;
using System.Text;

namespace AlertToCareAutomatedTesting.Models
{
    class VitalLogs
    {
        public int VitalsLogId { get; set; }
        public int PatientId { get; set; }
        public double BpmRate { get; set; }
        public double Spo2Rate { get; set; }
        public double RespRate { get; set; }
    }
}
