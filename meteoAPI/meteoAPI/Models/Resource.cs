﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace meteoAPI.Models
{
    public abstract class Resource
    {
        [JsonProperty(Order =-2)]
        public string Href { get; set; }
    }
}
