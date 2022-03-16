using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using My_Blog.Data.Repository;
using My_Blog.Models;
using My_Blog.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace My_Blog.Controllers
{
    [Authorize(Roles ="Admin")]
    public class PostController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private string _imagePath;
        public PostController(IUnitOfWork _unitOfWork, IConfiguration config)
        {
            unitOfWork = _unitOfWork;
            _imagePath = config["Path:Images"];
        }
        public async Task<IActionResult> Index()
        {
            var postList = await unitOfWork.Post.GetAll();
            return View(postList);
        }
        public async Task<IActionResult> CreateEdit(int? id)
        {
            if(id != null && id > 0)
            {
                var model = await unitOfWork.Post.GetById(id??0);

                var post = new PostViewModel {
                    Id = model.Id,
                    CategoryId = model.CategoryId,
                    Title = model.Title,
                    Description = model.Description,
                    Body = model.Body,
                    ImagePath =model.Image,
                    Tags = model.Tags
                };
                DropdownList(post);


                return View(post);
            }
            else
            {
                var post = new PostViewModel();
                DropdownList(post);
                return View(post);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEdit(PostViewModel model)
        {
            var imagePath = "";
            if(model.Image != null)
            {
                imagePath = UploadedFile(model.Image);
            }
            var post = new Post
            {
                Id= model.Id,
                CategoryId = model.CategoryId,
                Title = model.Title,
                Description=model.Description,
                Body = model.Body,
                Image = imagePath,
                Tags = model.Tags
            };
            if (model.Id > 0)
            {
               unitOfWork.Post.Update(post);
            }
            else
            {
                await unitOfWork.Post.Add(post);
            }
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await unitOfWork.Post.Delete(id);
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public void DropdownList(PostViewModel postViewModel)
        {
            var categoryList = unitOfWork.Category.GetAll().Result;
            var list = new List<SelectListItem>();
            foreach (var item in categoryList)
            {
                list.Add(new SelectListItem { Text = item.CategoryName, Value = item.Id.ToString() });
            }
            postViewModel.CategoryList = list;
        }

        private string UploadedFile(IFormFile image)
        {
            string uniqueFileName = null;
            string uploadPath = Path.Combine(_imagePath);
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
            string filePath = Path.Combine(uploadPath, uniqueFileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            image.CopyTo(fileStream);
            return uniqueFileName;
        }
    }
}
