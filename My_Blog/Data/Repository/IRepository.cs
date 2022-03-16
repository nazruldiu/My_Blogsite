using My_Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Blog.Data.Repository
{
    public interface IRepository
    {
        Post GetPost(int Id);
        List<Post> GetAllPost();
        void AddPost(Post post);
        void DeletePost(int Id);
        void UpdatePost(Post post);

        List<Category> GetAllCategory();
        void AddCategory(Category category);
        void DeleteCategory(int Id);
        void UpdateCategory(Category category);

        Task<bool> SaveChngesAsync();
    }
}
