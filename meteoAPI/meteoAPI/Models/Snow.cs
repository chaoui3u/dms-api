﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace meteoAPI.Models
{
    public class Snow : Resource
    {
        public int Id { get; set; }
        public float Volume { get; set; }
    }
}