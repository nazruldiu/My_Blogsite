using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Blog.Data.Repository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; set; }
        IPostRepository Post { get; set; }
        Task SaveChangesAsync();
    }
}
