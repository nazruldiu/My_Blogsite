using My_Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Blog.Data.Repository
{
    public class CategoryRepository: Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDBContext appDB): base(appDB) { }


        
    }
}
