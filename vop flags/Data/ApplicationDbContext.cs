using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using vop_flags.Models;

namespace vop_flags.Data
{
    public class ApplicationDbContext : DbContext
    {
        internal readonly object FlagDesign;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Flagdesign> Flagdesign { get; set; }

    }
}
