﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace meteoAPI.Models
{
    public class Weather : Resource
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
    }
}
