using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace meteoAPI.Models
{
    public class Sun : Resource
    {
       public DateTimeOffset SunRise { get; set; }
       public DateTimeOffset SunSet { get; set; }
    }
}
