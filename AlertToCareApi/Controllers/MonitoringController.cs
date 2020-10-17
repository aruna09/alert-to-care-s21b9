using AlertToCareApi.Models;
using AlertToCareApi.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AlertToCareApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitoringController : ControllerBase
    {

        //with regrad to vitals
        [HttpGet("HealthStatus")]
        public IEnumerable<string> GetAlarmForAllPatients()
        {
            List<string> messages = new List<string>();
            VitalsMonitoring vitalsMonitoring = new VitalsMonitoring();
            IEnumerable<PatientVitals> patientVitals = vitalsMonitoring.GetAllVitals();
            foreach(PatientVitals vitals in patientVitals)
            {
               messages.Add(vitalsMonitoring.CheckVitals(vitals));
            }
            return messages;
        }

        [HttpGet("HealthStatus/{PatientId}")]
        public string GetAlarmForParticularPatient(int patientId)
        {
            VitalsMonitoring vitalsMonitoring = new VitalsMonitoring();
            PatientVitals patientVitals = vitalsMonitoring.GetVitalsForSpecificPatient(patientId);
            string alarmMessage = vitalsMonitoring.CheckVitals(patientVitals);
            return alarmMessage;
        }

        //TODO:one alarm function with regard to any error

        //TODO:one utility function for manually closing the alarm


    }
}
