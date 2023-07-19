using ECommerce.Data;
using ECommerce.Helpers;
using ECommerce.Models;
using ECommerce.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext context;

        public AdminController(AppDbContext context)
        {
            this.context = context;
        }

        public IActionResult AddProduct()
        {
            ViewBag.Categories = new SelectList(context.Categories, "Id", "Name");
            ViewBag.Tags = new SelectList(context.Tags, "Id", "Name");
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

                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return View("error.cshtml");
            }
        }


        public IActionResult AddCategory()
        {
            return View();
        }


        [HttpPost]


        [HttpPost]
        public IActionResult AddCategory(AddCategoryViewModel model)
        {
            Category category = new()
            {
                Name = model.Name,
                CreatedTime = DateTime.Now
            };
            context.Add(category);
            context.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult DeleteProduct()
        {
            return View();
        }


        [HttpPost]
        public IActionResult DeleteProduct(int Id)
        {
            var entity = context.Categories.FirstOrDefault(x => x.Id == Id);
            if (entity is not null)
                context.Categories.Remove(entity);
            context.SaveChanges();

            return RedirectToAction("Index");
        }



        public IActionResult EditCategory()
        {
            return View();
        }
        public IActionResult EditCategory(EditCategoryViewModel model)
        {
            Category category = new()
            {
                Name = model.Name,
                CreatedTime = DateTime.Now
            };

            return RedirectToAction("Index");
        }







        public IActionResult AddTag()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddTag(AddTagViewModel model)
        {
            Tag tag = new()
            {
                Name = model.Name,
                CreatedTime = DateTime.Now
            };
            context.Add(tag);
            context.SaveChanges();

            return RedirectToAction("Index");
        }



        public IActionResult Index()
        {
            return View(context.Categories.ToList());
        }


        public IActionResult DeleteCategory(int Id)
        {
            var entity = context.Categories.FirstOrDefault(x => x.Id == Id);
            if (entity is not null)
                context.Categories.Remove(entity);
            context.SaveChanges();

            return RedirectToAction("Index");
            
        }





    }
}
