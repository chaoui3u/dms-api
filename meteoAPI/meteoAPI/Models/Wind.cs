using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace meteoAPI.Models
{
    public class Wind : Resource
    {
        public Guid Id { get; set; }
        public float Speed { get; set; }
        public float Degree { get; set; }
    }
}
