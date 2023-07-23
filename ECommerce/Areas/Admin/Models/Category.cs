using ECommerce.Areas.Admin.Models.ViewModels;

namespace ECommerce.Areas.Admin.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public virtual IEnumerable<Product> Products { get; set; }
    }
}