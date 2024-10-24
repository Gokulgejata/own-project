using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vopflag.Application.Contracts.Persistence;
using Vopflag.Domain.Models;
using Vopflag.Infrastructure.Common;
using Vopflag.Infrastructure.Repositories;

namespace Vopflag.Infrastructure.UnitOFWork
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly ApplicationDbContext _dbContext;
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext; 
            Flagdesign = new FlagDesignRepository(_dbContext);
            FlagMaterial=new FlagMaterialRepository(_dbContext);
            Post = new PostRepository(_dbContext);
              
        }
        public IFlagdesignRepository Flagdesign {  get; private set; }

        public IFlagMaterialRepository FlagMaterial { get; private set; }

        public IPostRepository Post { get; private set; }

        public void Dispose()

        {
           _dbContext.Dispose();
        }

        public async Task saveAsync()
        {
           await _dbContext.SaveChangesAsync();
        }
    }
}

