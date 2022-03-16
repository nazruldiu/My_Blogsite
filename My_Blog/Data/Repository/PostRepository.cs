using Microsoft.EntityFrameworkCore;
using My_Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Blog.Data.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(AppDBContext appDB) : base(appDB) { }

        //public override async Post GetById(int Id)
        //{
        //    return await  _dbSet.Where(x => x.Id == Id).FirstOrDefaultAsync();
        //}
    }
}
