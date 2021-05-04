using System;
using System.Collections.Generic;

namespace FlightManagementApi.Models
{
    public partial class Users
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public string Password { get; set; }
    }
}
