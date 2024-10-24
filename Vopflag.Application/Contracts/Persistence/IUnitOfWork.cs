using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vopflag.Application.Contracts.Persistence
{
    public interface IUnitOfWork: IDisposable
    {
        public IFlagdesignRepository Flagdesign {  get; }
        public IFlagMaterialRepository FlagMaterial { get; }
        public IPostRepository Post { get; }

        Task saveAsync();
    }
}
