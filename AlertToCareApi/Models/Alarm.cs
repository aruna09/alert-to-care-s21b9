using System.Collections.Generic;

namespace AlertToCareApi.Models
{
    public class Alarm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<string> Messages { get; set; }

    }
}
