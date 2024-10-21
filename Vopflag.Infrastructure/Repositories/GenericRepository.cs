﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Vopflag.Application.Contracts.Persistance;
using Vopflag.Domain.Common;
using Vopflag.Infrastructure.Common;

namespace Vopflag.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository <T> where T : BaseModel


    {
        protected readonly ApplicationDbContext _dbContext;
        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(T entity)
        {
            await _dbContext.AddAsync(entity);
        }

        public async Task Delete(T entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();

        }


        public async Task<List<T>> Get(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<bool> IsRecordExists(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate).AnyAsync();
        }
        public IEnumerable<T> Query(Expression<Func<T, bool>> predicate)
        {
            var entities = _dbContext.Set<T>().Where(predicate).ToList();
            return entities;
        }

        public IEnumerable<T> Query()
        {
            var entities = _dbContext.Set<T>().AsNoTracking().ToList();
            return entities;
        }
    }
}