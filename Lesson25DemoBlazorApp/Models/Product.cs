using System.ComponentModel.DataAnnotations;

namespace Lesson25DemoBlazorApp.Models
{
    public class Product
    {
        public int Id { get; set; }          // Primary key
        [Required]
        public string Name { get; set; } = string.Empty;
        [Range(1.00, 1000, ErrorMessage = "Price must be between $1.00 and $1000.00")]
        public decimal Price { get; set; }
    }

}
