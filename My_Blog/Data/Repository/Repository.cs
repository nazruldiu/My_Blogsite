using Microsoft.EntityFrameworkCore;
using My_Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Blog.Data.Repository
{
    public class Repository<T> : IRepository<T> where T:class
    {
        private AppDBContext _appDB;
        internal DbSet<T> _dbSet;

        public Repository(AppDBContext appDB)
        {
            _appDB = appDB;
            _dbSet = _appDB.Set<T>();
        }

        public async Task<T> GetById(int Id)
        {
           return await _dbSet.FindAsync(Id);
        }

        public async Task<bool> Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            return true;
        }

        public async Task<bool> Delete(int Id)
        {
            var entity = await _dbSet.FindAsync(Id);
            _dbSet.Remove(entity);
            return true;
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public bool Update(T entity)
        {
            _dbSet.Update(entity);
            return true;
        }

        //public async Task<bool> SaveChngesAsync()
        //{
        //    if (await _appDB.SaveChangesAsync() > 0)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

    }
}
