using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace meteoAPI.Models
{
    public class WeatherRecord :Resource
    {
        public MainData MainData { get; set; }
        public Wind Wind { get; set; }
        public DateTimeOffset CurrentTime { get; set; }
    }
}
