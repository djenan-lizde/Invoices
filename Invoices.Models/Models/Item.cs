using System.ComponentModel.DataAnnotations;

namespace Invoices.Models.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MinLength(3, ErrorMessage = "Description should be minimum 3 characters")]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(1, float.MaxValue, ErrorMessage = ("Please enter valid float Number"))]
        public float Price { get; set; }
    }
}
