using Food_World.DBContext;
using Food_World.Models;
using Microsoft.AspNetCore.Mvc;

namespace Food_World.Controllers
{
    public class AdminController : Controller
    {
        private readonly FoodWorldDbContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(FoodWorldDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            var products = _context.tblFoodItems.OrderBy(p => p.ItemName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var totalProducts = _context.tblFoodItems.Count();

            var model = new Pagination<FoodItemsEntity>(products, totalProducts, page, pageSize);

            return View(model);
        }

        public IActionResult Create()
        {
            TempData["AddSuccess"] = null;
            TempData["AddError"] = null;
            TempData["UpdateSuccess"] = null;
            TempData["UpdateError"] = null;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FoodItemsEntity fooditem, IFormFile? productImage)
        {
            TempData["AddSuccess"] = null;
            TempData["AddError"] = null;

            if (ModelState.IsValid)
            {
                try
                {
                    // Check if a file is uploaded
                    if (productImage is { Length: > 0 })
                    {
                        // Generate a unique filename for the image
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(productImage.FileName);

                        // Get the path of the wwwroot/img folder where images will be stored
                        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                        // Combine the unique filename with the path to store the image
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        // Copy the uploaded file to the specified path
                        await using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await productImage.CopyToAsync(stream);
                        }

                        // Update the ImageUrl property of the product with the new filename
                        fooditem.ImageUrl = uniqueFileName;
                    }

                    // Set CreateDateTime and ModifieDateTime
                    fooditem.CreatedDateTime = DateTime.Now;
                    fooditem.ModifiedDateTime = DateTime.Now;

                    // Add the product to the database
                    _context.tblFoodItems.Add(fooditem);
                    await _context.SaveChangesAsync();

                    TempData["AddSuccess"] = $"Food Item {fooditem.Id} is added to Menu.";

                    // Redirect to the product list page after successful creation
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Log or handle the exception appropriately
                    TempData["AddError"] = $"Exception while adding the new Food Item : {ex.Message}";
                }
            }

            // If the model state is not valid, return the view with the model data and errors
            return View(fooditem);
        }


        // Action method to display form for updating product details
        public IActionResult Edit(int id)
        {
            TempData["AddSuccess"] = null;
            TempData["AddError"] = null;
            TempData["UpdateSuccess"] = null;
            TempData["UpdateError"] = null;
            var product = _context.tblFoodItems.Find(id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, FoodItemsEntity fooditem)
        {
            TempData["UpdateSuccess"] = null;
            TempData["UpdateError"] = null;

            if (id != fooditem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Update other properties of the product as usual
                    _context.Update(fooditem);
                    await _context.SaveChangesAsync();
                    TempData["UpdateSuccess"] = $"Food Item {fooditem.Id} is updated successfully.";
                }
                catch (Exception ex)
                {
                    TempData["UpdateError"] = $"Exception updating the Food Item : {ex.Message}.";
                }
            }
            return RedirectToAction("Index");
        }


        // Action method to delete a product
        public IActionResult Delete(int id)
        {
            TempData["AddSuccess"] = null;
            TempData["AddError"] = null;
            TempData["UpdateSuccess"] = null;
            TempData["UpdateError"] = null;

            var product = _context.tblFoodItems.Find(id);
            if (product != null) _context.tblFoodItems.Remove(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
