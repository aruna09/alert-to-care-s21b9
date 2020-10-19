using AlertToCareApi.Models;
using AlertToCareApi.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AlertToCareApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitoringController : ControllerBase
    {
        readonly ConfigDbContext _context = new ConfigDbContext();


        //with regrad to vitals
        [HttpGet("HealthStatus")]
        public IEnumerable<string> GetAlarmForAllPatients()
        {
            List<string> messages = new List<string>();
            VitalsMonitoring vitalsMonitoring = new VitalsMonitoring();
            IEnumerable<VitalsLogs> patientVitals = vitalsMonitoring.GetAllVitals();
            foreach(VitalsLogs vitals in patientVitals)
            {
               messages.Add(vitalsMonitoring.CheckVitals(vitals));
            }
            return messages;
        }

        [HttpGet("HealthStatus/{PatientId}")]
        public IEnumerable<string> GetAlarmForParticularPatient(int patientId)
        {
            VitalsMonitoring vitalsMonitoring = new VitalsMonitoring();
            var patientStore = _context.Patients.Where(item => item.PatientId == patientId).FirstOrDefault();
            var monStat = patientStore.MonitoringStatus;
            if (monStat == 0)
            {
                List<string> patientVitalsAlarms = vitalsMonitoring.GetVitalsForSpecificPatient(patientId);
                return patientVitalsAlarms;
            }
            else
            {
                List<string> msg = new List<string>();
                msg.Add("No Alarms : Patient's Monitoring Status is Off ");
                return msg;
            }
            
        }

        //TODO:one alarm function with regard to any error

        //TODO:one utility function for manually closing the alarm

        [HttpGet("SetAlarmOn/{patientId}")]
        public int SetAlarmOn(int patientId)
        {
            try
            {
                var patientStore = _context.Patients.Where(item => item.PatientId == patientId);
                foreach (Patients patient in patientStore)
                {

                    patient.MonitoringStatus = 0;
                    _context.SaveChanges();
                }
                return 200;
            }
            catch
            {
                return 404;
            }
        }

        [HttpGet("SetAlarmOff/{patientId}")]
        public int SetAlarmOff(int patientId)
        {
            try
            {
                var patientStore = _context.Patients.Where(item => item.PatientId == patientId);
                foreach (Patients patient in patientStore)
                {

                    patient.MonitoringStatus = 1;
                    _context.SaveChanges();
                }
                return 200;
            }
            catch
            {
                return 404;
            }
        }
    }
}
