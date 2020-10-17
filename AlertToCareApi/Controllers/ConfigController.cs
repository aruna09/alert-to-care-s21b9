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
        static readonly List<Layouts> Layouts = new List<Layouts>
        {
            new Layouts{ LayoutId = 1 , CapacityLevel = "VERY HIGH", LayoutType = "CLUSTER"},
            new Layouts{ LayoutId = 2 , CapacityLevel = "HIGH" , LayoutType = "TRIANGULAR" },
            new Layouts{ LayoutId = 3 , CapacityLevel = "LOW" , LayoutType = "U-SHAPED" },
            new Layouts{ LayoutId = 4 , CapacityLevel = "VERY LOW" , LayoutType = "RADIAL" }
        };

        readonly ConfigDbContext _context = new ConfigDbContext();

        //Number of beds in each ICU
        [HttpGet("Beds")]
        public IEnumerable<NumberOfBedsInIcu> GetNumberOfBedsInEachIcu()
        {
            var bedStore = _context.Beds.ToList();
            var icuStore = _context.IcuRoom.ToList();
            var bedsInEachIcu = from bed in bedStore
                        group bed by bed.IcuRoomNo into bd
                        join icu in icuStore on bd.FirstOrDefault().IcuRoomNo equals icu.IcuRoomNo
                        select new NumberOfBedsInIcu
                        {
                            IcuRoomNo = icu.IcuRoomNo,
                            CountOfBeds = bd.Count(m=>m.IcuRoomNo==icu.IcuRoomNo)
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
                    bedIdentification.IcuRoomNo = bed.IcuRoomNo;
                    bedIdentification.OccupancyStatus = bed.OccupancyStatus;
                    bedIdentification.BedSerialNo = MapBedToLayout(bed.LayoutId);
                }
            }
            return bedIdentification;
        }

        public int MapBedToLayout(int layoutId)
        {
            int bedSerialNo = layoutId;
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
            return Layouts;
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
