using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace meteoAPI.Models
{
    public class WeatherRecordEntity
    {
        public Guid Id { get; set; }
        public CloudsEntity Clouds { get; set; }
        public MainDataEntity MainData { get; set; }
        public RainEntity Rain { get; set; }
        public SnowEntity Snow { get; set; }
        public SunEntity Sun { get; set; }
        public WeatherEntity Weather { get; set; }
        public WindEntity Wind { get; set; }
        public DateTimeOffset CurrentTime { get; set; }
    }
}
