﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vopflag.Domain.Common;

namespace Vopflag.Application.Contracts.Persistance
{
    public interface IGenericRepository<T> where T : BaseModel
    {
        Task Create(T entity);
        Task Delete(T entity);
        Task<List<T>> Get(Expression<Func<T,bool>> Predicate);
        Task<List<T>> GetAllAsync();
        IEnumerable<T> Query(Expression<Func<T, bool>> Predicate);
        IEnumerable<T> Query();
        Task<T> GetByIdAsync(Guid Id);
        Task<bool> IsRecordExists(Expression<Func<T, bool>> Predicate);

    }
}
