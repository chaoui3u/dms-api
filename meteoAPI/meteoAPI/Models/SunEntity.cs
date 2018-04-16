using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace meteoAPI.Models
{
    public class SunEntity
    {
        public int Id { get; set; }
        public DateTimeOffset SunRise { get; set; }
        public DateTimeOffset SunSet { get; set; }
    }
}
