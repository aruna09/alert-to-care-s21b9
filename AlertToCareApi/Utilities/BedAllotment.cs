using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlertToCareApi.Models;

namespace AlertToCareApi.Utilities
{
    public class BedAllotment
    {
        readonly ConfigDbContext _context = new ConfigDbContext();
        
        public List<Beds> GetAvailableBeds()
        {
            var bedStore = _context.Beds.ToList();
            var availableBeds = bedStore.Where(item => item.OccupancyStatus == false).ToList();
            return availableBeds;
        }
        public void AllotBedToPatient(Patients patient)
        {
            var bedStore = _context.Beds.ToList();
            var bedToBeAlloted = bedStore.Where(item => item.BedId == patient.BedId).FirstOrDefault();
            UpdateBedOccupancyStatus(bedToBeAlloted, true);
        }
        public void EmptyTheBed(Patients patient)
        {
            var bedStore = _context.Beds.ToList();
            var bedToBeEmptied = bedStore.Where(item => item.BedId == patient.BedId).FirstOrDefault();
            UpdateBedOccupancyStatus(bedToBeEmptied, false);
        }
        public void UpdateBedOccupancyStatus(Beds bed, bool bedStatus)
        {
            bed.OccupancyStatus = bedStatus;
            _context.Update(bed);
            _context.SaveChanges();
        }

    }
}
