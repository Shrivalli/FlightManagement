using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcClient.Models
{
    public class FetchByLocation
    {
        [Required]
        public string FromLocation { get; set; }
        [Required]
        public string ToLocation { get; set; }
    }
}
