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
    public class PostRepository : GenericRepository<Post>, IPostRepository

    {
        public PostRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Post>> GetAllPosts()
        {
            return await _dbContext.Post.Include(x => x.flagdesign).Include(x => x.flagMaterial).ToListAsync();

        }

        public async Task<Post> GetPostById(Guid id)
        {

            return await _dbContext.Post.Include(x => x.flagdesign).Include(x => x.flagMaterial).FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task Update(Post post)
        {
            var objFromDb = await _dbContext.Post.FirstOrDefaultAsync(x => x.Id == post.Id);
            if (objFromDb != null)
            {
                objFromDb.FlagdesignId = post.FlagdesignId;
                objFromDb.FlagMaterialId = post.FlagMaterialId;
                objFromDb.Name = post.Name;
                objFromDb.FlagMaterialType = post.FlagMaterialType;
                objFromDb.BundleAvailability = post.BundleAvailability;
                objFromDb.SinglePiecePrice = post.SinglePiecePrice;
                objFromDb.SingleBundlePrice = post.SingleBundlePrice;
                objFromDb.Ratings = post.Ratings;

                if (post.FlagImage != null)
                {
                    objFromDb.FlagImage = post.FlagImage;
                }
                _dbContext.Update(objFromDb);


            }
        }
    }
}
