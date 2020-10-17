using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AlertToCareApi.Models;
using AlertToCareApi.Utilities;
using System.Threading;

namespace AlertToCareApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        static List<Layouts> layouts = new List<Layouts>
        {
            new Layouts{ layoutId = 1 , capacityLevel = "VERY HIGH", layoutType = "CLUSTER"},
            new Layouts{ layoutId = 2 , capacityLevel = "HIGH" , layoutType = "TRIANGULAR" },
            new Layouts{ layoutId = 3 , capacityLevel = "LOW" , layoutType = "U-SHAPED" },
            new Layouts{ layoutId = 4 , capacityLevel = "VERY LOW" , layoutType = "RADIAL" }
        };

        ConfigDbContext _context = new ConfigDbContext();

        //Number of beds in each ICU
        [HttpGet("Beds")]
        public IEnumerable<NumberOfBedsInIcu> GetNumberOfBedsInEachICU()
        {
            var bedStore = _context.Beds.ToList();
            var icuStore = _context.ICURoom.ToList();
            var bedsInEachIcu = from bed in bedStore
                        group bed by bed.icuRoomNo into bd
                        join icu in icuStore on bd.FirstOrDefault().icuRoomNo equals icu.icuRoomNo
                        select new NumberOfBedsInIcu
                        {
                            IcuRoomNo = icu.icuRoomNo,
                            CountOfBeds = bd.Count(m=>m.icuRoomNo==icu.icuRoomNo)
                        };

            return bedsInEachIcu;
        }

        //Bed Identification
        //TODO: Add the info about bed serial no by mapping it with the layout
        [HttpGet("Beds/{BedId}")]
        public BedIdentification GetParticularBedInfo(int bedId)
        {
            BedIdentification bedIdentification = new BedIdentification();
            var bedStore = _context.Beds.ToList();
            foreach(Beds bed in bedStore)
            {
                if(bed.BedId == bedId)
                {
                    bedIdentification.BedId = bed.BedId;
                    bedIdentification.icuRoomNo = bed.icuRoomNo;
                    bedIdentification.OccupancyStatus = bed.OccupancyStatus;
                    bedIdentification.BedSerialNo = MapBedToLayout(bed.LayoutId);
                }
            }
            return bedIdentification;
        }

        public int MapBedToLayout(int layoutId)
        {
            int bedSerialNo = 0;
            /*switch (layoutId)
            {
                case 1: bedSerialNo = 1;
                    break;
                case 2: bedSerialNo = 2;
                    break;
                case 3: bedSerialNo = 3;
                    break;
                case 4: bedSerialNo = 4;
                    break;
                default: bedSerialNo = 0;
                    break;
            }*/
            return bedSerialNo;
        }

        //Layout Information
        //TODO: Add the info about the number of beds and ICUs in each layout
        [HttpGet("Layouts")]
        public IEnumerable<Layouts> GetLayoutInfo()
        {
            return layouts;
        }

        [HttpPost("Beds")]
        public void AddNewBed([FromBody] Beds bed)
        {
            _context.Add(bed);
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
    }
}
