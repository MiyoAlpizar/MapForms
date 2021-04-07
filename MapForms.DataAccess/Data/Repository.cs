using AutoMapper;
using MapForms.DataAccess.Data.Repositories;
using MapForms.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MapForms.DataAccess.Data
{
    public class Repository<T> : IRepository<T> where T : class, IId, new()
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            dbSet = context.Set<T>();
        }

        public async Task<T> Add(T entity)
        {
            dbSet.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<TDTO> Add<TCreation, TDTO>(TCreation creation) where TCreation : class
        {
            var entity = mapper.Map<T>(creation);
            dbSet.Add(entity);
            await context.SaveChangesAsync();
            return await Get<TDTO>(entity.Id);
        }


        public async Task<TDTO> Get<TDTO>(int id)
        {
            var entity = await dbSet.FindAsync(id);
            return mapper.Map<TDTO>(entity);
        }

        public async Task<T> Get(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<T> GetFirstOrDefault(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetList(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> queryable = dbSet;
            if (filter != null)
            {
                queryable = queryable.Where(filter);
            }
            return await queryable.ToListAsync();
        }

        public async Task<T> Update<TCreation, TDTO>(int id, TCreation update)
        {
            var exists = await dbSet.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return null;
            }
            var entidad = mapper.Map<T>(update);
            entidad.Id = id;
            context.Entry(entidad).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entidad;
        }

        public async Task<T> Patch<TCreation, TDTO>(int id, Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<TDTO> patchDocument) where TDTO : class
        {
            if (patchDocument == null)
            {
                return null;
            }

            var entity = await dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return null;
            }

            var entityDTO = mapper.Map<TDTO>(entity);
            patchDocument.ApplyTo(entityDTO);
            mapper.Map(entityDTO, entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Remove(int id) 
        {
            var entity = await dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return false;
            }
            context.Remove(new T { Id = id });
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Remove(T entity)
        {
            return await Remove(entity.Id);
        }

       
    }
}
