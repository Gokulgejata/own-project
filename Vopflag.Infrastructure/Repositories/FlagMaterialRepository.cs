using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vopflag.Application.Contracts.Persistence;
using Vopflag.Domain.Models;
using Vopflag.Infrastructure.Common;

namespace Vopflag.Infrastructure.Repositories
{
    public class FlagMaterialRepository : GenericRepository<FlagMaterial>, IFlagMaterialRepository
    {
        public FlagMaterialRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task update(FlagMaterial flagMaterial)
        {
            var objFromDb = await _dbContext.FlagMaterial.FirstOrDefaultAsync(x => x.Id == flagMaterial.Id);
            if (objFromDb != null)
            {
                // Update the properties
                objFromDb.MaterialType = flagMaterial.MaterialType;

                // Mark the entity as modified
                _dbContext.Entry(objFromDb).State = EntityState.Modified;

                // Save the changes to the database
                await _dbContext.SaveChangesAsync();
            }
        }
    }

}
