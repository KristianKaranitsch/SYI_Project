using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace World_Weather.Models
{
    public class CityModel
    {
        [Required(ErrorMessage = "Bitte wählen Sie eine Stadt aus.")]
        public string SelectedCity { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }
    }
}
