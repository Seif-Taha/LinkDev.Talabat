﻿using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey>(StoreContext _dbContext) : IGenericRepository<TEntity, TKey>
        where TEntity : BaseAuditableEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                withTracking? await _dbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).ToListAsync() :
                              await _dbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).AsNoTracking().ToListAsync();
            }

            withTracking? await _dbContext.Set<Product>().ToListAsync() :
                          await _dbContext.Set<Product>().AsNoTracking().ToListAsync();

        }

        ///{
        ///
        ///    if (withTracking) return await _dbContext.Set<TEntity>().ToListAsync();
        ///
        ///    return await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        ///
        ///}

        public async Task<TEntity?> GetAsync(TKey id)
        {

            if (typeof(TEntity) == typeof(Product))
            {
                return await _dbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).FirstOrDefaultAsync(P => P.Id == id);
            }

            return await _dbContext.Set<TEntity>().FindAsync(id);

        }


        public async Task AddAsync(TEntity entity) => await _dbContext.Set<TEntity>().AddAsync(entity);

        public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);

        public void Delete(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);

    }
}
