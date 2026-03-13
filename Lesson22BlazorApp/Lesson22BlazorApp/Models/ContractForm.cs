using System.ComponentModel.DataAnnotations;

namespace Lesson22BlazorApp.Models
{
    public class ContactForm
    {
        [Required]  // DataAnnotations 
        public string Name { get; set; } = "";

        [Range(18, 120)] // DataAnnotations 
        public int Age { get; set; }

    }
}
