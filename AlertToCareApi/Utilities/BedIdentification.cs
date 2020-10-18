using AlertToCareApi.Models;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Collections.Generic;
using System.Linq;

namespace AlertToCareApi.Utilities
{
    public class BedIdentification
    {
        readonly ConfigDbContext _context = new ConfigDbContext();

        public int FindLayoutIdForBed(int icuNo)
        {
            int layoutId = 0;
            var icuStore = _context.Icu.ToList();
            foreach(Icu icu in icuStore)
            {
                if(icu.IcuNo == icuNo)
                {
                    layoutId = icu.LayoutId;
                }
            }
            return layoutId;
        }
        public int FindCapacityOfLayout(int icuNo)
        {
            int capacity = 0;
            int layoutId = FindLayoutIdForBed(icuNo);
            var layoutStore = LayoutInformation.Layouts;
            foreach (Layouts layouts in layoutStore)
            {
                if (layouts.LayoutId == layoutId)
                {
                    capacity = layouts.Capacity;
                }
            }
            return capacity;
        }

        public int FindCountOfBeds(int icuNo)
        {
            var bedStore = _context.Beds.ToList();
            var bedsInEachIcu = bedStore.Where(item => item.IcuNo == icuNo).Count(); 
            return bedsInEachIcu;
        }

        public int FindBedSerialNo(int icuNo)
        {
            int capacity = FindCapacityOfLayout(icuNo);
            int countofbeds = FindCountOfBeds(icuNo);
            int serialNo = 0;
            if(capacity == countofbeds)
            {
                //SendAlarmMessage("The ICU is Full, Cannot Add More Beds");
            }
            else
            {
                serialNo = countofbeds + 1;
            }
            return serialNo;

        }
    }
}
