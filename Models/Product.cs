using System.ComponentModel.DataAnnotations;

namespace useCookieAuth.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [MinLength(3, ErrorMessage ="Product Name must at least 3 characters")]
        public string ProductName { get; set; }

        [Required(ErrorMessage ="Product haven't price")]
        public double ProductPrice { set; get; }
        public string ProductDescription { get; set; }

    }
}
