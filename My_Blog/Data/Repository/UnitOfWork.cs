using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Blog.Data.Repository
{
    public class UnitOfWork: IUnitOfWork, IDisposable
    {
        private AppDBContext _appDB;
        public ICategoryRepository Category { get; set; }
        public IPostRepository Post { get; set; }

        public UnitOfWork(AppDBContext appDB)
        {
            _appDB = appDB;
            Category = new CategoryRepository(_appDB);
            Post =  new PostRepository(_appDB);
        }


        public async Task SaveChangesAsync()
        {
            await _appDB.SaveChangesAsync();
        }

        public void Dispose()
        {
            _appDB.Dispose();
        }
    }
}
