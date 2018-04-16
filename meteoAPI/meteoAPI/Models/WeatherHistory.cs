using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace meteoAPI.Models
{
    public class WeatherHistory : Resource
    {
        public int Id { get; set; }
        public DateTimeOffset Dt { get; set; }
        public Weather Weather { get; set; }
        public Sun Sun { get; set; }
        public MainData Main { get; set; }
        public Wind Wind { get; set; }
        public Clouds Clouds { get; set; }
        public Rain Rain { get; set; }
        public Snow Snow { get; set; }
    }
}
