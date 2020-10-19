using System;
using System.Collections.Generic;
using System.Text;

namespace AlertToCareAutomatedTesting.Models
{
    class Layouts
    {
        public int LayoutId { get; set; }
        public string LayoutType { get; set; }
        public int Capacity { get; set; }
        public int NoOfIcus { get; set; }
        public List<Icu> ListOfIcus { get; set; }
    }
}
