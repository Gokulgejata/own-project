using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vopflag.Application.Contracts.Persistance;
using Vopflag.Domain.Models;

namespace Vopflag.Application.Contracts.Persistence
{
    public interface IFlagMaterialRepository:IGenericRepository<FlagMaterial>
    {
        Task update(FlagMaterial flagMaterial); 
    }
}
