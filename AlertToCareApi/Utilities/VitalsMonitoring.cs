using AlertToCareApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlertToCareApi
{
    public class VitalsMonitoring
    {
        List<PatientVitals> patientVitals = new List<PatientVitals>
        {
            new PatientVitals{ vitalsInfoId = 1, patientId = 1, bpmRate = 23, respRate = 60, spo2Rate = 90},
            new PatientVitals{ vitalsInfoId = 2, patientId = 3, bpmRate = 78, respRate = 120, spo2Rate = 56}
        };
        public IEnumerable<PatientVitals> GetAllVitals()
        {
            return patientVitals;
        }
        public PatientVitals GetVitalsForSpecificPatient(int id)
        {
            foreach(PatientVitals vitals in patientVitals)
            {
                if(vitals.patientId == id)
                {
                    return vitals;
                }
            }
            return null;
        }

        public string CheckVitals(PatientVitals vital)
        {
            var a = CheckSpo2(vital.spo2Rate);
            var b = CheckBpm(vital.bpmRate);
            var c = CheckRespRate(vital.respRate);
            var s = a + "," + b + "," + c;
            return s;
        }
        public string CheckSpo2(double spo2)
        {
            if (spo2 < 90)
            {

                return "Spo2 is low ";

            }
            else
                return "";

        }
        public string CheckBpm(double bpm)
        {
            if (bpm < 70)
                return "bpm is low ";
            if (bpm > 150)
                return "bpm is high ";
            else
                return "";
        }
        public string CheckRespRate(double respRate)
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
