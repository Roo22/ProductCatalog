using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApplication5.Data;

namespace WebApplication5.Models.Repos
{
    public class CategoryDbRepo :IEntityDBRepo<Category>
    {
        DataContext context;
        public CategoryDbRepo(DataContext _db)
        {
            context = _db;
        }
        public void Add(Category entity)
        {
            context.Categories.Add(entity);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var category = Find(id);
            context.Categories.Remove(category);
            context.SaveChanges();
        }

        public Category Find(int id)
        {
            var category = context.Categories.SingleOrDefault(x => x.Id == id);
            return category;
        }

        public List<Category> List()
        {
            return context.Categories.ToList();
        }

        public List<Category> Search(string term)
        {
           return context.Categories.Include(p => p.Product)
                .Where(p => p.Name.ToLower().Contains(term.ToLower()))
                .ToList();

        }

        public void Update(int id, Category entity)
        {
            context.Categories.Update(entity);
            context.SaveChanges();
        }
    }
}
