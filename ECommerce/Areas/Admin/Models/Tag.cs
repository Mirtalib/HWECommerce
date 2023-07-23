using ECommerce.Areas.Admin.Models.ViewModels;

namespace ECommerce.Areas.Admin.Models
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public virtual IEnumerable<ProductTag> ProductTags { get; set; }
    }
}
