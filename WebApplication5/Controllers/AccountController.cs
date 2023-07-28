using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
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
        private readonly RoleManager<IdentityRole> roleManager;
        ProductDbRepo productDbRepo;
        DataContext dataContext;
        public AccountController(UserManager<User> UserManager, 
            SignInManager<User> signInManager, 
            DataContext dataContext,
            RoleManager<IdentityRole> roleManager)
        {
            this.UserManager = UserManager;
            this.signInManager = signInManager;
            this.dataContext = dataContext;
            this.roleManager = roleManager;
            
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(User Authuser)
        {

            var user = await UserManager.FindByEmailAsync(Authuser.Email);

            if (user is null)
            {
                var appUser = new User
                {
                    Email = Authuser.Email,
                    Name = Authuser.Name,
                    UserId = Authuser.UserId
                };
                var addToDB = dataContext.User.Add(appUser);
                var addToDb2 = dataContext.Add(appUser);
                dataContext.SaveChanges();
                var result = await UserManager.CreateAsync(appUser, Authuser.Password);

                if (result.Succeeded)
                {
                    // Create the "User" role if it doesn't exist
                    var roleExists = await roleManager.RoleExistsAsync("User");

                    if (!roleExists)
                    {
                        var role = new IdentityRole("User");
                        await roleManager.CreateAsync(role);
                        dataContext.SaveChanges();
                    }

                    // Assign the role to the user
                    await UserManager.AddToRoleAsync(appUser, "User");
                    dataContext.SaveChanges();

                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, appUser.Name),
                new Claim(ClaimTypes.Role, "User")
            };

                    var identity = new ClaimsIdentity(claims, "anyvalue");
                    var principle = new ClaimsPrincipal(identity);
                    dataContext.SaveChanges();

                    await HttpContext.SignInAsync("cookie", principle);

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
