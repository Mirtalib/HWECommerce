using System.ComponentModel.DataAnnotations;

namespace ECommerce.Areas.Admin.Models.ViewModels
{
    public class AddProductViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Image Url is required")]
        public IFormFile ImageUrl { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Tag is required")]
        public int[] TagIds { get; set; }
    }
}
