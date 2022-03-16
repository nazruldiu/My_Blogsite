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
        private readonly IUnitOfWork unitOfWork;
        public CategoryController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var categoryList = await unitOfWork.Category.GetAll();
            return View(categoryList);
        }

        [HttpGet]
        public IActionResult CreateEdit()
        {
            var model = new CategoryViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEdit(CategoryViewModel model)
        {
            var category = new Category
            {
                Id = model.Id,
                CategoryName = model.CategoryName
            };
            await unitOfWork.Category.Add(category);
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
