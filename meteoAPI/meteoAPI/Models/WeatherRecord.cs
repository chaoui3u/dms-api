using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace meteoAPI.Models
{
    public class WeatherRecord :Resource
    {
        public Link Clouds { get; set; }
        public Link MainData { get; set; }
        public Link Rain { get; set; }
        public Link Snow { get; set; }
        public Link Sun { get; set; }
        public Link Weather { get; set; }
        public Link Wind { get; set; }
        public DateTimeOffset CurrentTime { get; set; }
    }
}
