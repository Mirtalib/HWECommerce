using System.ComponentModel.DataAnnotations;

namespace ECommerce.Areas.Admin.Models.ViewModels
{
    public class AddTagViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }
}
