using AlertToCareApi.Models;
using System.Collections.Generic;

namespace AlertToCareApi.Utilities
{
    public class VitalsMonitoring
    {
        readonly List<PatientVitals> patientVitals = new List<PatientVitals>
        {
            new PatientVitals{ VitalsInfoId = 1, PatientId = 1, BpmRate = 23, RespRate = 60, Spo2Rate = 90},
            new PatientVitals{ VitalsInfoId = 2, PatientId = 3, BpmRate = 78, RespRate = 120, Spo2Rate = 56}
        };
        public IEnumerable<PatientVitals> GetAllVitals()
        {
            return patientVitals;
        }
        public PatientVitals GetVitalsForSpecificPatient(int id)
        {
            foreach(PatientVitals vitals in patientVitals)
            {
                if(vitals.PatientId == id)
                {
                    return vitals;
                }
            }
            return null;
        }

        public string CheckVitals(PatientVitals vital)
        {
            var a = CheckSpo2(vital.Spo2Rate);
            var b = CheckBpm(vital.BpmRate);
            var c = CheckRespRate(vital.RespRate);
            var s = a + "," + b + "," + c;
            return s;
        }
        private string CheckSpo2(double spo2)
        {
            if (spo2 < 90)
            {

                return "Spo2 is low ";

            }
            else
                return "";

        }
        private string CheckBpm(double bpm)
        {
            if (bpm < 70)
                return "bpm is low ";
            if (bpm > 150)
                return "bpm is high ";
            else
                return "";
        }
        private string CheckRespRate(double respRate)
        {
            if (respRate < 30)
                return "respRate is low ";
            if (respRate > 95)
                return "respRate is high ";
            else
                return "";
        }
    }
}
