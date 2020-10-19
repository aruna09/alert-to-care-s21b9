using AlertToCareApi.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlertToCareApi.EntriesValidator
{
    public class LayoutValidator
    {
        public static bool CheckIfLayoutIsPresent(int layoutId)
        {
            var layoutStore = LayoutInformation.Layouts;
            if (layoutStore.FirstOrDefault(item => item.LayoutId == layoutId) == null)
            {
                return false;
            }
            return true;
        }
    }
}
