using AlertToCareApi.EntriesValidator;
using AlertToCareApi.Models;
using AlertToCareApi.Utilities;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

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
        public ActionResult<IEnumerable<NumberOfBedsInIcu>> GetNumberOfBedsInEachIcu()
        {
            try
            {
                var icuStore = _context.Icu.ToList();
                List<NumberOfBedsInIcu> numberOfBedsInIcu = new List<NumberOfBedsInIcu>();
                BedIdentification bedIdentification = new BedIdentification();
                foreach (Icu icu in icuStore)
                {
                    numberOfBedsInIcu.Add(new NumberOfBedsInIcu { IcuRoomNo = icu.IcuNo, CountOfBeds = bedIdentification.FindCountOfBeds(icu.IcuNo) });
                }
                return Ok(numberOfBedsInIcu);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        //Bed Identification
        [HttpGet("Beds/{BedId}")]
        public ActionResult<Beds> GetParticularBedInfo(int bedId)
        {
            try
            {
                var bedStore = _context.Beds.ToList();
                var bed = bedStore.Where(item => item.BedId == bedId).FirstOrDefault();
                if (bed == null)
                {
                    return BadRequest("No Bed With The Given Bed Id Is Present");
                }
                return Ok(bed);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        //Layout Information
        [HttpGet("Layouts")]
        public ActionResult<IEnumerable<Layouts>> GetLayoutInfo()
        {
            try
            {
                LayoutInformation layoutInformation = new LayoutInformation();
                var layoutStore = LayoutInformation.Layouts;
                foreach (Layouts layout in layoutStore)
                {
                    layout.NoOfIcus = layoutInformation.FindNoOfIcus(layout.LayoutId);
                    layout.ListOfIcus = layoutInformation.FindListOfIcus(layout.LayoutId);
                }
                return Ok(layoutStore);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        #endregion

        #region Manipulation Functions

        [HttpGet("Beds")]
        public ActionResult<IEnumerable<Beds>> GetAllBedsInfo()
        {
            try
            {
                return Ok(_context.Beds.ToList());
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("Icu")]
        public ActionResult<IEnumerable<Icu>> GetAllIcuInfo()
        {
            try
            {
                return Ok(_context.Icu.ToList());
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("Beds")]
        public IActionResult AddNewBed([FromBody] Beds bed)
        {
            try
            {
                BedIdentification bedIdentification = new BedIdentification();
                if (!IcuValidator.CheckIfIcuIsPresent(bed.IcuNo))
                {
                    return BadRequest("The Inserted ICU No Is Not Available");
                }
                else
                {
                    bed.BedSerialNo = bedIdentification.FindBedSerialNo(bed.IcuNo);
                    if (bed.BedSerialNo == 0)
                    {
                        return BadRequest("No More Beds Can be Added In This ICU Layout, ICU Is Full");
                    }
                    _context.Add(bed);
                    _context.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("Icu")]
        public IActionResult AddNewIcu([FromBody] Icu icu)
        {
            try
            {
                if (!LayoutValidator.CheckIfLayoutIsPresent(icu.LayoutId))
                {
                    return BadRequest("The Inserted Layout Id For The ICU is Not Available");
                }
                else
                {
                    _context.Add(icu);
                    _context.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        #endregion
    }
}
