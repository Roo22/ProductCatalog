using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication5.Data;
using WebApplication5.Domain;
using WebApplication5.Models;
using WebApplication5.Models.Repos;

namespace WebApplication5.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public UserManager<AppUser> UserManager;
        private readonly SignInManager<AppUser> signInManager;
        ProductDbRepo productDbRepo;
        DataContext dataContext;
        public HomeController(ILogger<HomeController> logger,UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, DataContext dataContext)
        {
            _logger = logger;
            this.UserManager = userManager;
            this.signInManager = signInManager;
            this.dataContext = dataContext;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var products = productDbRepo.List();

            if (products != null)
            {
                return View();
            }
            else
            {
                ViewData.Add("CommingSoon", "Comming Soon");
                return View();
            }       
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(User Authuser)
        {
            
            var user = dataContext.User.SingleOrDefault(x => x.Email == Authuser.Email);

            if (user is null)
            {
                var appUser = new AppUser
                {
                    Email = Authuser.Email,
                    FirstName = Authuser.Name,
                    LastName = Authuser.Name,
                };

                    var result = await UserManager.CreateAsync(appUser, Authuser.Password);
                
                var addToDB = dataContext.User.Add(Authuser);
                var addToDb2 = dataContext.Add(appUser);
                dataContext.SaveChanges();
                if (result.Succeeded)
                {
                       var claims = new List<Claim>(2)
                       {
                           new Claim(ClaimTypes.Name, appUser.FirstName),
                           new Claim(ClaimTypes.Role,"User")
                       };
                       var identity = new ClaimsIdentity(claims,"anyvalue");
                       var principle = new ClaimsPrincipal(identity);
                       HttpContext.SignInAsync("cookie",principle);
                       await UserManager.AddToRoleAsync(appUser, "User");
                    dataContext.SaveChanges(true);
                    return RedirectToAction("Index", "Login");
                }
               
            }
            else
            {
                ViewData.Add("Error", "Email is already taken");
                return View(Authuser);
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User Authuser)
        {
            var user = await dataContext.User.FirstOrDefaultAsync(x => x.Email == Authuser.Email && x.Password == Authuser.Password);
            if (user!= null)
            {
                ViewBag.username = string.Format("Successfully logged-in",Authuser.Name);

                TempData["username"] = "none";
                return RedirectToAction("Index");
            }
        
            else
            {
                ViewBag.username = string.Format("Login Failed ");
                return View();
            }
            return View();
        }
        public async Task SignOut()
        {
            await signInManager.SignOutAsync();
        }
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await SignOut();
            return RedirectToAction("Index");
        }
        public ActionResult Search(string term)
        {
            var result = productDbRepo.Search(term);
            return View("Index", result);
        }
        public ActionResult Details(int id)
        {
            var product = productDbRepo.Find(id);
            return View(product);
        }

    }
}
