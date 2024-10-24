using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vopflag.Application.Contracts.Persistance;
using Vopflag.Domain.Models;

namespace Vopflag.Application.Contracts.Persistence
{
    public interface IPostRepository : IGenericRepository <Post>
    {
        Task Update(Post post); 
        Task <Post>GetPostById (Guid id);
        Task <List<Post>> GetAllPosts();
    }
}
