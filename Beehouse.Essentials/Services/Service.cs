using Beehouse.Essentials.Entities;
using Beehouse.Essentials.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beehouse.Essentials.Services
{
    public class Service<TEntity> : IService<TEntity> where TEntity : Entity
    {
        private DbSet<TEntity> _entities;

        protected readonly DbContext Context;

        protected DbSet<TEntity> Entities => _entities ?? (_entities = Context.Set<TEntity>());

        public Service(DbContext context)
        {
            Context = context;
        }

        public IQueryable<TEntity> GetEntities()
        {
            return Entities;
        }

        public IQueryable<TEntity> GetEntities(bool tracking)
        {
            return tracking ? Entities.AsTracking() : Entities.AsNoTracking();
        }

        public Task<bool> Exists(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> GetById(string id)
        {
            return await Entities.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public Task Delete(string id, bool logic = false)
        {
            throw new NotImplementedException();
        }

        public async Task<ListResult<TEntity>> Get()
        {
            var result = new ListResult<TEntity>
            {
                Count = await Entities.CountAsync()
            };

            if (result.Count == 0) return result;

            result.List = await Entities.ToListAsync();

            return result;
        }

        public async Task<ListResult<TEntity>> Get(IQueryable<TEntity> query)
        {
            query ??= GetEntities();
            var result = new ListResult<TEntity>
            {
                Count = await query.CountAsync()
            };

            if (result.Count == 0) return result;

            result.List = await query.ToListAsync();

            return result;
        }

        public async Task<ListResult<TEntity>> Get(int page, int limit, IQueryable<TEntity> query)
        {
            query ??= GetEntities();
            var result = new ListResult<TEntity>
            {
                Page = page,
                Limit = limit,
                Count = await query.CountAsync()
            };

            if (result.Count == 0) return result;

            result.List = await query.Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();

            return result;
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            entity.CreatedAt = DateTime.Now;
            entity.ModifiedAt = DateTime.Now;

            await Entities.AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            var entry = Context.Entry(entity);
            entry.Property(_ => _.CreatedAt).IsModified = false;

            entity.ModifiedAt = DateTime.Now;

            await Context.SaveChangesAsync();

            return entity;
        }
    }
}
