using System;
using System.Collections.Generic;

namespace FlightManagementApi.Models
{
    public partial class Airlines
    {
        public int AirlineId { get; set; }
        public string AirlineName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
