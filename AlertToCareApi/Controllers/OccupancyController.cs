using AlertToCareApi.Models;
using AlertToCareApi.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AlertToCareApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OccupancyController : ControllerBase
    {
        readonly ConfigDbContext _context = new ConfigDbContext();

        #region MainFunctions

        [HttpGet("AvailableBeds")]
        public IEnumerable<Beds> GetBedsAvaliability()
        {
            BedAllotment bedAllotment = new BedAllotment();
            return bedAllotment.GetAvailableBeds();
        }

        [HttpGet("Status/{BedId}")]
        public bool GetBedsOccupancyStatus(int bedId)
        {
            var bedStore = _context.Beds.ToList();
            var status = bedStore.Where(item => item.BedId == bedId).FirstOrDefault().OccupancyStatus;
            return status;

        }

        [HttpPost("PatientInfo")]
        public void AddPatientInfo([FromBody] Patients patient)
        {
            BedAllotment bedAllotment = new BedAllotment();
            List<Beds> availableBeds = bedAllotment.GetAvailableBeds();
            if (availableBeds.Count == 0)
            {
                //AlertMessage("No beds are available, cannot add more patients")
            }
            else
            {
                patient.BedId = availableBeds[0].BedId;
                _context.Patients.Add(patient);
                _context.SaveChanges();
                bedAllotment.AllotBedToPatient(patient);
            }
        }

        [HttpDelete("PatientInfo/{patientId}")]
        public void DischargingPatient(int patientId)
        {
            BedAllotment bedAllotment = new BedAllotment();
            var patientStore = _context.Patients.ToList();
            foreach (Patients patient in patientStore)
            {
                if (patient.PatientId == patientId)
                {
                    bedAllotment.EmptyTheBed(patient);
                    _context.Remove(patient);
                    _context.SaveChanges();
                }
            }
        }

        #endregion

        #region ManipulationFunctions


        //Get All Patient Info
        [HttpGet("PatientInfo")]
        public IEnumerable<Patients> GetAllPatientInfo()
        {
            var patientStore = _context.Patients.ToList();
            return patientStore;
        }

        //Particular Patient Info 
        [HttpGet("PatientInfo/{patientId}")]
        public Patients GetParticularPatientInfo(int patientId)
        {
            var patientStore = _context.Patients.ToList();
            foreach (Patients patient in patientStore)
            {
                if (patient.PatientId == patientId)
                {
                    return patient;
                }
            }
            return null;
        }

        //Updating a Patient Info
        [HttpPut("PatientInfo/{patientId}")]
        public void UpdateParticularPatientInfo(int patientId, [FromBody] Patients updatedPatient)
        {
            var patientStore = _context.Patients.ToList();
            foreach (Patients patient in patientStore)
            {
                if (patient.PatientId == patientId)
                {
                    _context.Update(updatedPatient);
                    _context.SaveChanges();
                }
            }
        }
        #endregion

    }
}
