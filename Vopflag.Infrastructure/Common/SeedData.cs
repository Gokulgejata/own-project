using Microsoft.EntityFrameworkCore;
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
