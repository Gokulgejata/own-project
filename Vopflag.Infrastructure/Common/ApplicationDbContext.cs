﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vopflag.Domain.Models;

namespace Vopflag.Infrastructure.Common
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