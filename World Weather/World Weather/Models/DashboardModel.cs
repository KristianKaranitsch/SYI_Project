using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace World_Weather.Models
{
    public class DashboardModel
    {
        public double Wien { get; set; }
        public double NewYork { get; set; }
        public double Sydney { get; set; }
        public double Tokio { get; set; }
        public List<string> CityPickOptions { get; set; }

        public string SelectedCapital { get; set; }
    }
}
