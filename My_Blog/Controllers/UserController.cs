using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using My_Blog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Blog.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;
        public UserController(SignInManager<IdentityUser> _signInManager)
        {
            signInManager = _signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            return View(loginViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var result = await signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, false, false);
            if (!result.Succeeded)
            {
                return View(loginViewModel);
            }
            return RedirectToAction("Index","Home");
        }
    }
}
