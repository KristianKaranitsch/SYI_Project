using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace World_Weather.Models
{
    public class DashboardModel
    {
        public int Wien { get; set; }
        public int NewYork { get; set; }
        public int Sydney { get; set; }
        public int Tokio { get; set; }
        public List<string> CityPickOptions { get; set; }

        public string SelectedCapital { get; set; }
    }
}
