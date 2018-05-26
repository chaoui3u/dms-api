using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace meteoAPI.Models
{
    public class RootResponse :Resource
    {
        public Link Signup { get; set; }
        public Link Users { get; set; }
        public Link WeatherRecord { get; set; }
    }
}
