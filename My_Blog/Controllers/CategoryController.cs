using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Blog.Data.Repository;
using My_Blog.Models;
using My_Blog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Blog.Controllers
{
    [Authorize(Roles ="Admin ")]
    public class CategoryController : Controller
    {
        private readonly IRepository repository;
        public CategoryController(IRepository _repository)
        {
            repository = _repository;
        }
        public IActionResult Index()
        {
            var categoryList = repository.GetAllCategory();
            return View(categoryList);
        }

        [HttpGet]
        public IActionResult CreateEdit()
        {
            var model = new CategoryViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateEdit(CategoryViewModel model)
        {
            var category = new Category
            {
                Id = model.Id,
                CategoryName = model.CategoryName
            };
            repository.AddCategory(category);
            repository.SaveChngesAsync();
            return RedirectToAction("Index");
        }
    }
}
