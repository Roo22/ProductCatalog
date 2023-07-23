using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
    public class AccountController : Controller
    {
        private readonly UserManager<User> UserManager;
        private readonly SignInManager<User> signInManager;
        ProductDbRepo productDbRepo;
        DataContext dataContext;
        public AccountController(UserManager<User> UserManager, SignInManager<User> signInManager, DataContext dataContext)
        {
            this.UserManager = UserManager;
            this.signInManager = signInManager;
            this.dataContext = dataContext;
            
        }
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
                var appUser = new User
                {
                    Email = Authuser.Email,
                    Name= Authuser.Name,
                    UserId = Authuser.UserId
                };

                var result = await UserManager.CreateAsync(appUser, Authuser.Password);

                var addToDB = dataContext.User.Add(Authuser);
                var addToDb2 = dataContext.Add(appUser);
                dataContext.SaveChanges();
                if (result.Succeeded)
                {
                    var claims = new List<Claim>(2)
                       {
                           new Claim(ClaimTypes.Name, appUser.Name),
                           new Claim(ClaimTypes.Role,"User")
                       };
                    var identity = new ClaimsIdentity(claims, "anyvalue");
                    var principle = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync("cookie", principle);
                    await UserManager.AddToRoleAsync(appUser, "User");
                    dataContext.SaveChanges(true);
                    return RedirectToAction("Login", "Account");
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
            if (user != null)
            {
                var claims = new List<Claim>(2)
                       {
                           new Claim(ClaimTypes.Name, Authuser.Name),
                       };
                var identity = new ClaimsIdentity(claims, "anyvalue");
                var principle = new ClaimsPrincipal(identity);

                HttpContext.SignInAsync("cookie", principle);
                ViewBag.username = string.Format("Successfully logged-in", Authuser.Name);

                TempData["username"] = "none";
                return RedirectToAction("Index","Home");
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
            return RedirectToAction("Index","Home");
        }

    }
 
}
