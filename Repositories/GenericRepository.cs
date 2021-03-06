using BlogApi.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlogApi.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private ApplicationDbContext _context;
        private DbSet<T> table = null;
       // private GenericRepository<T> repository;

        public GenericRepository(GenericRepository<T> repository)
        {
            _context = new ApplicationDbContext();
            table = _context.Set<T>();
        }

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }

        public void Delete(int id)
        {
            var item = GetById(id);
            table.Remove(item);
        }

        public List<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(int id)
        {
           return table.Find(id);
        }

        public void Insert(T model)
        {
            table.Add(model);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(T model)
        {
            table.Attach(model);
            _context.Entry(model).State= EntityState.Modified;
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this._context.Set<T>().Where(expression).AsNoTracking();
        }

        public IList<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return this._context.Set<T>().Where(expression).ToList();
        }

    }
}
