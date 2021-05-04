using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApi.Models
{
    public class EFDbContext:DbContext
    {
        public EFDbContext(DbContextOptions<EFDbContext> options):base(options)
        {

        }
        public virtual DbSet<Bookings> Bookings { get; set; }
        public virtual DbSet<Flights> Flights { get; set; }
    }
}
