using Microsoft.AspNetCore.Mvc;
using My_Blog.Data.Repository;
using My_Blog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Blog.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            var post = _repository.GetAllPost();
            return View(post);
        }
        public IActionResult Post(int id)
        {
            var model = _repository.GetPost(id);
            var post = new PostViewModel
            {
                Id = model.Id,
                CategoryId = model.CategoryId,
                Title = model.Title,
                Description = model.Description,
                Body = model.Body,
                Tags = model.Tags,
                ImagePath = model.Image,
                CreatedDate = model.CreatedDate
            };
            return View(post);
        }
    }
}
