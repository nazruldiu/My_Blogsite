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
        private readonly IRepository repository;
        private string _imagePath;
        public PostController(IRepository _repository, IConfiguration config)
        {
            repository = _repository;
            _imagePath = config["Path:Images"];
        }
        public IActionResult Index()
        {
            var postList = repository.GetAllPost();
            return View(postList);
        }
        public IActionResult CreateEdit(int? id)
        {
            if(id != null && id > 0)
            {
                var model = repository.GetPost(id??0);
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
        public IActionResult CreateEdit(PostViewModel model)
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
                repository.UpdatePost(post);
            }
            else
            {
                repository.AddPost(post);
            }
            repository.SaveChngesAsync();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            repository.DeletePost(id);
            repository.SaveChngesAsync();
            return RedirectToAction("Index");
        }

        public void DropdownList(PostViewModel postViewModel)
        {
            var categoryList = repository.GetAllCategory();
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
