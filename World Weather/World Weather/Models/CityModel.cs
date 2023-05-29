using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace World_Weather.Models
{
    public class CityModel
    {

        public string CapitalName { get; set; }

        public string CapitalLongitude { get; set; }

        public string CapitalLatitude { get; set; }
    }
}
