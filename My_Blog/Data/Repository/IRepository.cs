using My_Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Blog.Data.Repository
{
    public interface IRepository<T> where T: class
    {
        Task<T> GetById(int Id);
        Task<IEnumerable<T>> GetAll();
        Task<bool> Add(T entity);
        Task<bool> Delete(int Id);
        bool Update(T entity);
        //Task<bool> SaveChngesAsync();
    }
}
