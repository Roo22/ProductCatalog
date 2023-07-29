using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication5.Models;
using WebApplication5.Models.Repos;

namespace WebApplication5.Controllers
{
    [Authorize (AuthenticationSchemes ="cookie",Roles = "Admin")]
    public class AdminController : Controller
    {
        ProductDbRepo ProductDbRepo;
        public IActionResult Index()
        {
            var products = ProductDbRepo.List();

            return View(products);
        }
        public ActionResult Details(int id)
        {
            var product = ProductDbRepo.Find(id);
            return View(product);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, int id)
        {
           try
            {
                product.UserId = id;
                ProductDbRepo.Add(product);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            var product = ProductDbRepo.Find(id);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            try
            {
                ProductDbRepo.Update(id, product);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            ProductDbRepo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
