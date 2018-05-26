using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace meteoAPI.Models
{
    public class MainDataEntity
    {
        public Guid Id { get; set; }
        public float Temp { get; set; }
        public int Humidity { get; set; }
        public float Pressure { get; set; }

    }
}
