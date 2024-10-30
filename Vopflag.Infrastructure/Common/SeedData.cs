using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vopflag.Domain.Models;

namespace Vopflag.Infrastructure.Common
{
    public class SeedData
    {
        public  static async Task SeedRole(IServiceProvider serviceProvider)
        {
            var Scope= serviceProvider.CreateScope();
            var roleManager= Scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var Roles=

        }
        public static async Task SeedDataAsync(ApplicationDbContext _dbContext)
        {
            if (!_dbContext.FlagMaterial.Any())
            {
                await _dbContext.FlagMaterial.AddRangeAsync(
                new FlagMaterial
                {
                    MaterialType = "Plastic"
                },
                new FlagMaterial
                {

                    MaterialType = "Paper"
                });
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
