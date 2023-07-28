using System;

namespace WebApplication5.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public int Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }


        public int UserId { get; set; }
        public User User { get; set; } 

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
