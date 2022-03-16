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
        private readonly IUnitOfWork unitOfWork;

        public HomeController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var post = await unitOfWork.Post.GetAll();
            return View(post);
        }
        public async Task<IActionResult> Post(int id)
        {
            var model = await unitOfWork.Post.GetById(id);
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
