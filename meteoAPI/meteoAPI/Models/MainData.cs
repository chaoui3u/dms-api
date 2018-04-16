using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace meteoAPI.Models
{
    public class MainData : Resource
    {
        public int Id { get; set; }
        public float Temp { get; set; }
        public int Humidity { get; set; }
        public float TempMin { get; set; }
        public float TempMax { get; set; }
        public float Pressure { get; set; }
        public float SeaLevel { get; set; }
        public float GrndLevel { get; set; }
    }
}
