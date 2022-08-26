using System.ComponentModel.DataAnnotations;

namespace useCookieAuth.Models
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }

        [Required]
        [MaxLength(120, ErrorMessage = "Brand Name no more 120 character"), MinLength(3, ErrorMessage ="Brand Name must at least 3 character")]
        public string BrandName { get; set; }
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
