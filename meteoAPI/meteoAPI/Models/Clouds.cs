﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace meteoAPI.Models
{
    public class Clouds : Resource
    {
        
        public int Id { get; set; }
        public double All { get; set; }
    }
}