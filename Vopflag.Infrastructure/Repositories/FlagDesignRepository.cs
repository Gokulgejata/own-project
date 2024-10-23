using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Vopflag.Application.Contracts.Persistence;
using Vopflag.Domain.Models;
using Vopflag.Infrastructure.Common;

namespace Vopflag.Infrastructure.Repositories
{
    public class FlagDesignRepository : GenericRepository<Flagdesign>, IFlagdesignRepository
    {
        public FlagDesignRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task update(Flagdesign flagdesign)
        {
            var objFromDb = await _dbContext.Flagdesign.FirstOrDefaultAsync(x => x.Id == flagdesign.Id);

            if (objFromDb != null)
            {
                objFromDb.FlagName = flagdesign.FlagName;
                objFromDb.Types = flagdesign.Types;

                if (flagdesign.Flagview != null)
                {
                    objFromDb.Flagview = flagdesign.Flagview;
                }

                _dbContext.Update(objFromDb);
                await _dbContext.SaveChangesAsync();  // Ensure changes are saved
            }
        }

    }
}