using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlertToCareApi.EntriesValidator
{
    public class IcuValidator
    {
        public static bool CheckIfIcuIsPresent(int icuNo)
        {
            ConfigDbContext _context = new ConfigDbContext();
            var icuStore = _context.Icu.ToList();
            if (icuStore.FirstOrDefault(item => item.IcuNo == icuNo) == null)
            {
                return false;
            }
            return true;
        }
    }
}
