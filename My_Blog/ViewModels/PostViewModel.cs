using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Blog.ViewModels
{
    public class PostViewModel
    {
        public PostViewModel()
        {
            this.CategoryList = new List<SelectListItem>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public string Tags { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<SelectListItem> CategoryList { get; set; }
    }
}
