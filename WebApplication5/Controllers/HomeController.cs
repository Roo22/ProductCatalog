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
        ProductDbRepo productDbRepo;
        
        
        
        public async Task<IActionResult> IndexAsync()
        {
            if (productDbRepo != null)
            {
                var products = productDbRepo.List();

                if (products != null)
                {
                    return View();
                }
            }

            ViewData.Add("CommingSoon", "Comming Soon");
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
    
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
