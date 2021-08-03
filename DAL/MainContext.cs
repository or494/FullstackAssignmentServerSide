using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MainContext : DbContext
    {
        public DbSet<QueuePlace> QueuePlaces { get; set; }

        public MainContext(DbContextOptions<MainContext> options) : base(options) { }
    }
}
