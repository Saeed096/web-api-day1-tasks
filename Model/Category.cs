using System.ComponentModel.DataAnnotations;

namespace task1.Model
{
    public class Category
    {
        public int Id { get; set; }
        [MinLength(2, ErrorMessage ="name must be 2 characters at least")]
        public string Name { get; set; }
        public List<Product>? Products { get; set; } 
    }
}
