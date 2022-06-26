using backend_api.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_api.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public Repository(ApplicationDbContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }
        protected DbContext Context { get; }
        protected DbSet<T> DbSet { get; }
        public T Add(T entity)
        {
            DbSet.Add(entity);
            SaveChanges();
            return entity;
        }

        public void AddRange(IEnumerable<T> entities)
        {
            DbSet.AddRange(entities);
            SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet.ToList();
        }

        public T GetById(object id)
        {
            return DbSet.Find(id);
        }

        public IQueryable<T> Query()
        {
            return DbSet;
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
            SaveChanges();
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
            SaveChanges();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }

        public T Update(T entity)
        {
            DbSet.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
            return entity;
        }
    }
}
