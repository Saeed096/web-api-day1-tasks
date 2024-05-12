using System.ComponentModel.DataAnnotations.Schema;

namespace task1.Model
{
    public class Product 
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        [ForeignKey("category")]
        public int? CategoryId { get; set; } 
        public Category? category { get; set; }

    }
}
