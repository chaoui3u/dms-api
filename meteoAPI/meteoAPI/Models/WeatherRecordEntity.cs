using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace meteoAPI.Models
{
    public class WeatherRecordEntity
    {
        public Guid Id { get; set; }
        public MainDataEntity MainData { get; set; }
        public WindEntity Wind { get; set; }
        public DateTimeOffset CurrentTime { get; set; }
    }
}
