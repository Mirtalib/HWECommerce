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
        public IActionResult Index()
        {
            return View();
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
            if (ModelState.IsValid)
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

                    return RedirectToAction("ViewProduct");
                }
                catch (Exception)
                {

                    return View("error.cshtml");
                }
            }
            else
            {
                return View();
            }
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

            return RedirectToAction("ViewProduct");
        }

        public IActionResult ViewProduct(int Id)
        {
            return View(context.Products.ToList());
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(AddCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {

                Category category = new()
                {
                    Name = model.Name,
                    CreatedTime = DateTime.Now
                };
                context.Add(category);
                context.SaveChanges();

                return RedirectToAction("ViewCategory");
            }
            return View();
        }

        public IActionResult DeleteCategory(int Id)
        {
            var entity = context.Categories.FirstOrDefault(x => x.Id == Id);
            if (entity is not null)
                context.Categories.Remove(entity);
            context.SaveChanges();

            return RedirectToAction("ViewCategory");
            
        }

        public IActionResult ViewCategory(int Id)
        {
            return View(context.Categories.ToList());
        }

        public IActionResult AddTag()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddTag(AddTagViewModel model)
        {
            if (ModelState.IsValid)
            {
                Tag tag = new()
                {
                    Name = model.Name,
                    CreatedTime = DateTime.Now
                };
                context.Add(tag);
                context.SaveChanges();

                return RedirectToAction("ViewTag");
            }
            else
            {
                return View();
            }
        }

        public IActionResult DeleteTag(int id)
        {
            var entity = context.Tags.FirstOrDefault(x => x.Id == id);
            if (entity is not null)
                context.Tags.Remove(entity);
            context.SaveChanges();
            return RedirectToAction("ViewTag");
        }


        public IActionResult ViewTag()
        {
            return View(context.Tags.ToList());
        }


    }
}
