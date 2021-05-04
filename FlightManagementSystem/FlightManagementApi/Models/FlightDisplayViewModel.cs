using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManagementApi.Models
{
    public class FlightDisplayViewModel
    {
        public Flights Flights { get; set; }
        public string AirlineName { get; set; }
    }
}
