using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace meteoAPI.Models
{
    public class Link
    {
        public const string GetMethod = "Get";

        public static Link To(string routeName, object routeValues = null)
            => new Link
            {
                RouteName = routeName,
                RouteValue = routeValues,
                Method = GetMethod,
                Relations = null
            };

        [JsonProperty(Order = -4)]
        public string Href { get; set; }
        [JsonProperty(Order = -3, NullValueHandling = NullValueHandling.Ignore , DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(GetMethod)]
        public string Method { get; set; }
        [JsonProperty(Order = -2 ,PropertyName = "rel", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Relations { get; set; }

        //store route name before being rewritten
        [JsonIgnore]
        public string RouteName { get; set; }

        [JsonIgnore]
        public object RouteValue { get; set; }
    }
}
