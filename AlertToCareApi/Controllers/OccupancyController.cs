using AlertToCareApi.Models;
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

        [HttpGet("AvailableBeds")]
        public IEnumerable<Beds> GetBedsAvaliability()
        {
            List<Beds> avaliableBeds = new List<Beds>();
            var bedStore = _context.Beds.ToList();
            foreach(Beds bed in bedStore)
            {
                if(bed.OccupancyStatus == true)
                {
                    avaliableBeds.Add(bed);
                }
            }
            return avaliableBeds;
        }

        [HttpGet("Beds/{BedId}")]
        public bool GetBedsOccupancyStatus(int bedId)
        {
            var bedStore = _context.Beds.ToList();
            bool status = false;
            foreach (Beds bed in bedStore)
            {
                if (bed.BedId == bedId)
                {
                    status = bed.OccupancyStatus;
                }
            }
            return status;

        }

        //Get All Patient Info
        [HttpGet("PatientInfo")]
        public IEnumerable<Patients> Get()
        {
            var patientStore = _context.Patient.ToList();
            return patientStore;
        }

        // Particular Layout Info 
        [HttpGet("PatientInfo/{patientId}")]
        public Patients Get(int patientId)
        {
            var patientStore = _context.Patient.ToList();
            foreach (Patients patient in patientStore)
            {
                if (patient.PatientId == patientId)
                {
                    return patient;
                }
            }
            return null;
        }

        //Adding a Patient Info
        [HttpPost("PatientInfo")]
        public void Post([FromBody] Patients patient)
        {
            _context.Patient.Add(patient);
            _context.SaveChanges();
            AllotBedToPatient(patient);
        }

        //Updating a Patient Info
        [HttpPut("PatientInfo/{patientId}")]
        public void Put(int patientId, [FromBody] Patients updatedPatient)
        {
            var patientStore = _context.Patient.ToList();
            foreach (Patients patient in patientStore)
            {
                if (patient.PatientId == patientId)
                {
                    _context.Update(updatedPatient);
                    _context.SaveChanges();
                }
            }
        }

        //Delete a Patient Info
        [HttpDelete("PatientInfo/{patientId}")]
        public void DeleteLayout(int patientId)
        {
            var patientStore = _context.Patient.ToList();
            foreach (Patients patient in patientStore)
            {
                if (patient.PatientId == patientId)
                {
                    _context.Remove(patient);
                    _context.SaveChanges();
                }
            }
        }

        //Alloting Bed to Patient and then updating the bed occupancy status
        public void AllotBedToPatient(Patients patient)
        {
            var bedStore = _context.Beds.ToList();
            foreach(Beds bed in bedStore) 
            {
                if (patient.BedId == bed.BedId)
                {
                    UpdateBedOccupancyStatus(bed);
                }
            }
            //AlertUtility.SendAlertMessage(message);
            
        }

        public void UpdateBedOccupancyStatus(Beds bed)
        {
            bed.OccupancyStatus = true;
            _context.Update(bed);
            _context.SaveChanges();
        }

    }
}
