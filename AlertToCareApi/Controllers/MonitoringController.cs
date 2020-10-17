using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AlertToCareApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlertToCareApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitoringController : ControllerBase
    {
        List<string> alarmMessages = new List<string>();

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
