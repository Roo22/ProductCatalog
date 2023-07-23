using System.Collections.Generic;

namespace WebApplication5.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Product { get; set; }

    }
}
