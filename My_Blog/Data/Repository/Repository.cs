using Microsoft.EntityFrameworkCore;
using My_Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Blog.Data.Repository
{
    public class Repository : IRepository
    {
        private AppDBContext _appDB;

        public Repository(AppDBContext appDB)
        {
            _appDB = appDB;
        }
        public void AddPost(Post post)
        {
            _appDB.Post.Add(post);
        }
        public void DeletePost(int Id)
        {
            _appDB.Post.Remove(GetPost(Id));
        }

        public List<Post> GetAllPost()
        {
            return _appDB.Post.Include(m => m.Category).ToList();
        }

        public Post GetPost(int Id)
        {
            return _appDB.Post.FirstOrDefault(x=>x.Id == Id);
        }

        public void UpdatePost(Post post)
        {
            _appDB.Post.Update(post);
        }

        public async Task<bool> SaveChngesAsync()
        {
            if (await _appDB.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public List<Category> GetAllCategory()
        {
            return _appDB.Category.ToList();
        }

        public void AddCategory(Category category)
        {
            _appDB.Category.Add(category);
        }

        public void DeleteCategory(int Id)
        {
            _appDB.Category.Remove(_appDB.Category.FirstOrDefault(x=>x.Id == Id));
        }

        public void UpdateCategory(Category category)
        {
            _appDB.Category.Update(category);
        }
    }
}
