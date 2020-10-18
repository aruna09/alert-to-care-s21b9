using AlertToCareApi.Models;
using AlertToCareApi.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AlertToCareApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        readonly ConfigDbContext _context = new ConfigDbContext();

        #region MainFunctions

        //Number of beds in each ICU
        [HttpGet("BedsInEachIcu")]
        public IEnumerable<NumberOfBedsInIcu> GetNumberOfBedsInEachIcu()
        { 
            var icuStore = _context.Icu.ToList();
            List<NumberOfBedsInIcu> numberOfBedsInIcu = new List<NumberOfBedsInIcu>();
            BedIdentification bedIdentification = new BedIdentification();
            foreach(Icu icu in icuStore)
            {
                numberOfBedsInIcu.Add(new NumberOfBedsInIcu { IcuRoomNo = icu.IcuNo, CountOfBeds = bedIdentification.FindCountOfBeds(icu.IcuNo) });
            }
            return numberOfBedsInIcu;
        }

        //Bed Identification
        [HttpGet("Beds/{BedId}")]
        public Beds GetParticularBedInfo(int bedId)
        {
            var bedStore = _context.Beds.ToList();
            var bed = bedStore.Where(item => item.BedId == bedId).FirstOrDefault();
            return bed;
        }

        //Layout Information
        [HttpGet("Layouts")]
        public IEnumerable<Layouts> GetLayoutInfo()
        {
            LayoutInformation layoutInformation = new LayoutInformation();
            var layoutStore = LayoutInformation.Layouts;
            foreach(Layouts layout in layoutStore)
            {
                layout.NoOfIcus = layoutInformation.FindNoOfIcus(layout.LayoutId);
                layout.ListOfIcus = layoutInformation.FindListOfIcus(layout.LayoutId);
            }
            return layoutStore;
        }

        #endregion

        #region ManipulatingFunctions

        [HttpGet("Beds")]
        public IEnumerable<Beds> GetAllBedsInfo()
        {
            return _context.Beds.ToList();
        }

        [HttpGet("Icu")]
        public IEnumerable<Icu> GetAllIcuInfo()
        {
            return _context.Icu.ToList();
        }

        [HttpPost("Beds")]
        public void AddNewBed([FromBody] Beds bed)
        {
            BedIdentification bedIdentification = new BedIdentification();
            bed.BedSerialNo = bedIdentification.FindBedSerialNo(bed.IcuNo);
            _context.Add(bed);
            _context.SaveChanges();
        }

        [HttpPost("Icu")]
        public void AddNewIcu([FromBody] Icu icu)
        {
            _context.Add(icu);
            _context.SaveChanges();
        }

        [HttpPut("Beds/{BedId}")]
        public void UpdateParticularBedInfo(int bedId, [FromBody] Beds updatedBed)
        {
            var bedStore = _context.Beds.ToList();
            foreach (Beds bed in bedStore)
            {
                if (bed.BedId == bedId)
                {
                    _context.Update(updatedBed);
                    _context.SaveChanges();
                }
            }
        }

        [HttpPut("Icu/{IcuNo}")]
        public void UpdateParticularIcuInfo(int icuNo, [FromBody] Icu updatedIcu)
        {
            var icuStore = _context.Icu.ToList();
            foreach (Icu icu in icuStore)
            {
                if (icu.IcuNo == icuNo)
                {
                    _context.Update(updatedIcu);
                    _context.SaveChanges();
                }
            }
        }

        [HttpDelete("Beds/{BedId}")]
        public void DeleteParticularBedInfo(int bedId)
        {
            var bedStore = _context.Beds.ToList();
            foreach (Beds bed in bedStore)
            {
                if (bed.BedId == bedId)
                {
                    _context.Remove(bed);
                    _context.SaveChanges();
                }
            }
        }

        [HttpDelete("Icu/{IcuNo}")]
        public void DeleteParticularIcuInfo(int icuNo)
        {
            var icuStore = _context.Icu.ToList();
            foreach (Icu icu in icuStore)
            {
                if (icu.IcuNo == icuNo)
                {
                    _context.Remove(icu);
                    _context.SaveChanges();
                }
            }
        }
        #endregion
    }
}
