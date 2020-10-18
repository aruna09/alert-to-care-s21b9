using AlertToCareApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace AlertToCareApi.Utilities
{
    public class VitalsMonitoring
    {
        readonly ConfigDbContext _context = new ConfigDbContext();
        public IEnumerable<VitalsLogs> GetAllVitals()
        {
            return _context.VitalsLogs.ToList();
        }
        public VitalsLogs GetVitalsForSpecificPatient(int id)
        {
            var vitalStore = _context.VitalsLogs.ToList();
            foreach(VitalsLogs vitals in vitalStore)
            {
                if(vitals.PatientId == id)
                {
                    return vitals;
                }
            }
            return null;
        }

        public string CheckVitals(VitalsLogs vital)
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
