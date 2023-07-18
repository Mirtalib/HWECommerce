using ECommerce.Data;
using ECommerce.Helpers;
using ECommerce.Models;
using ECommerce.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerce.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext context;

        public AdminController(AppDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View(context.Products.ToList());
        }

        public IActionResult AddProduct()
        {
            ViewBag.Categories = new SelectList(context.Categories, "Id", "Name");
            ViewBag.Tags = new MultiSelectList(context.Tags, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductViewModel model)
        {
            try
            {
                string path = await UploadFileHelper.UploadFile(model.ImageUrl);

                Product product = new()
                {
                    Name = model.Name,
                    Description = model.Description,
                    ImageUrl = path,
                    CategoryId = model.CategoryId,
                };

                context.Add(product);
                await context.SaveChangesAsync();

                foreach (var tag in model.TagIds)
                {
                    context.Add(new ProductTag { TagId = tag, ProductId = product.Id });
                }

                await context.SaveChangesAsync();

                return View("Index.cshtml");
            }
            catch (Exception)
            {

                return View("error.cshtml");
            }
        }
    }
}
