using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace meteoAPI.Models
{
    public class WeatherRecord :Resource
    {
        public Clouds Clouds { get; set; }
        public MainData MainData { get; set; }
        public Rain Rain { get; set; }
        public Snow Snow { get; set; }
        public Sun Sun { get; set; }
        public Wind Wind { get; set; }
        public DateTimeOffset CurrentTime { get; set; }
    }
}
