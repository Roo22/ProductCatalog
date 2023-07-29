using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApplication5.Data;

namespace WebApplication5.Models.Repos
{
    public class ProductDbRepo : IEntityDBRepo<Product>
    {
        DataContext context;
        public ProductDbRepo(DataContext _db)
        {
            context = _db;
        }
        public void Add(Product entity)
        {
            context.Products.Add(entity);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = Find(id);
            context.Products.Remove(product);
            context.SaveChanges();
        }

        public Product Find(int id)
        {
            var product = context.Products.SingleOrDefault(x => x.Id == id);
            return product;
        }

        public List<Product> List()
        {
            return context.Products.ToList();
        }

        public List<Product> Search(string term)
        {
            return context.Products
                .Include(b => b.Category)
                .Where(b => b.Category.Name.ToLower() == term.ToLower())
                .ToList();
        }

        public void Update(int id, Product entity)
        {
            context.Products.Update(entity);
            context.SaveChanges();
        }
    }
}
