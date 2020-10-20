using AlertToCareApi.Models;
using System;
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
        public List<string> GetVitalsForSpecificPatient(int id)
        {
            var vitalStore = _context.VitalsLogs.ToList();
            var vitals = vitalStore.Where(item => item.PatientId == id).ToList();
            IEnumerable<VitalsLogs> patientVitals = vitals;
            List<string> alarms = new List<string>();
            foreach (VitalsLogs log in vitals.Skip(Math.Max(0, vitals.Count - 10)))
            {
                var pid = log.PatientId;
                var patient = _context.Patients.Where(item => item.PatientId == pid).FirstOrDefault();
                var pname = patient.PatientName;
                var spo2 = CheckSpo2(log.Spo2Rate);
                var bpm = CheckBpm(log.BpmRate);
                var respRate = CheckRespRate(log.RespRate);
                if (spo2 != 0 || bpm != 0 || respRate != 0)
                {
                    var tempMsg = "LogId : " + log.VitalsLogId + ", " + "PatientId : " + pid + ", " + "Name : " + pname + ", " + "SPO2 : " + InterpretMessage(spo2) + ", " + "BPM : " + InterpretMessage(bpm) + ", " + "RespRate : " + InterpretMessage(respRate);
                    alarms.Add(tempMsg);
                }

            }
            return alarms;
        }

        public string CheckVitals(VitalsLogs vital)
        {
            var pid = vital.PatientId;
            var patient = _context.Patients.Where(item => item.PatientId == pid).FirstOrDefault();
            var pname = patient.PatientName;
            var spo2 = CheckSpo2(vital.Spo2Rate);
            var bpm = CheckBpm(vital.BpmRate);
            var respRate = CheckRespRate(vital.RespRate);
            var a = "Spo2 Rate " + InterpretMessage(spo2);
            var b = "Bpm Rate " + InterpretMessage(bpm);
            var c = "Respiratory Rate " + InterpretMessage(respRate);
            var s = "" + pid + "," + pname + "," + a + "," + b + "," + c;
            return s;
        }
        private int CheckSpo2(double spo2)
        {
            if (spo2 < 95)
            {
                return -1;
            }
            else
                return 0;

        }
        private int CheckBpm(double bpm)
        {
            if (bpm < 70)
                return -1;
            if (bpm > 100)
                return 1;
            else
                return 0;
        }
        private int CheckRespRate(double respRate)
        {
            if (respRate < 12)
                return -1;
            if (respRate > 16)
                return 1;
            else
                return 0;
        }

        private string InterpretMessage(int msg)
        {
            if (msg == -1)
                return "is low";
            if (msg == 1)
                return "is high";
            else
                return "is good";
        }
    }
}
