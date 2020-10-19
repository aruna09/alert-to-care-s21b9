using AlertToCareApi.Models;
using AlertToCareApi.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public ActionResult<IEnumerable<string>> GetAlarmForAllPatients()
        {
            try
            {
                List<string> messages = new List<string>();
                VitalsMonitoring vitalsMonitoring = new VitalsMonitoring();
                IEnumerable<VitalsLogs> patientVitals = vitalsMonitoring.GetAllVitals();
                foreach (VitalsLogs vitals in patientVitals)
                {
                    messages.Add(vitalsMonitoring.CheckVitals(vitals));
                }
                return Ok(messages);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("HealthStatus/{PatientId}")]
        public ActionResult<IEnumerable<string>> GetAlarmForParticularPatient(int patientId)
        {
            try
            {
                VitalsMonitoring vitalsMonitoring = new VitalsMonitoring();
                var patientStore = _context.Patients.Where(item => item.PatientId == patientId).FirstOrDefault();
                var monStat = patientStore.MonitoringStatus;
                if (monStat == 0)
                {
                    List<string> patientVitalsAlarms = vitalsMonitoring.GetVitalsForSpecificPatient(patientId);
                    return Ok(patientVitalsAlarms);
                }
                else
                {
                    List<string> msg = new List<string>();
                    msg.Add("No Alarms : Patient's Monitoring Status is Off ");
                    return Ok(msg);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

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
                return 500;
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
