using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace meteoAPI.Models
{
    public class MesureEntity
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public string Id_Site { get; set; }
        public string Unit { get; set; }
        public string Phy_name { get; set; }
    }
}
