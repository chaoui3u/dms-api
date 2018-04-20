using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace meteoAPI.Models
{
    public class Clouds : Resource
    {
        
        public Guid Id { get; set; }
        public int All { get; set; }
    }
}
