using System.Linq;

namespace AlertToCareApi.Utilities
{
    public class BedIdentification
    {
        readonly ConfigDbContext _context = new ConfigDbContext();

        private int FindLayoutIdForBed(int icuNo)
        {
            var icuStore = _context.Icu.ToList();
            var layoutId = icuStore.FirstOrDefault(item => item.IcuNo == icuNo).LayoutId;
            return layoutId;
        }
        private int FindCapacityOfLayout(int icuNo)
        {
            int layoutId = FindLayoutIdForBed(icuNo);
            var layoutStore = LayoutInformation.Layouts;
            var capacity = layoutStore.Where(item => item.LayoutId == layoutId).FirstOrDefault().Capacity;
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
            if (capacity == countofbeds)
            {
                return 0;
            }
            else
            {
                int serialNo = countofbeds + 1;
                return serialNo;
            }


        }
    }
}
