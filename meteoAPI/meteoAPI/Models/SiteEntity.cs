using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace meteoAPI.Models
{
    public class SiteEntity :Resource
    {
        public string Id { get; set; }
        public string Refrence { get; set; }
        public string Label { get; set; }
        public double Latitude { get; set; }
        public double Logitude { get; set; }
        public string Type { get; set; }
        public string Classification { get; set; }
        public string Area { get; set; }
    }
}
