using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Database.Entities;

namespace ButcherApp.Data
{
    public class ButcherAppContext : DbContext
    {
        public ButcherAppContext (DbContextOptions<ButcherAppContext> options)
            : base(options)
        {
        }

        public DbSet<Database.Entities.Animal> Animals { get; set; } = default!;
    }
}
