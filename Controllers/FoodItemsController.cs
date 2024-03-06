using Food_World.DBContext;
using Food_World.Models;
using Food_World.Models.EFDBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Food_World.Controllers
{
    public class FoodItemsController : Controller
    {
        private readonly FoodWorldDbContext _context;
        public FoodItemsController(FoodWorldDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            TempData["NoProducts"] = null;
            // Retrieve all products from the database
            var fooditems = _context.tblFoodItems.ToList();

            // Check if products list is empty
            if (fooditems.Count == 0)
            {
                // If products list is empty, set an error message using ViewBag
                TempData["NoProducts"] = "Currently no food items available.";
                return View();
            }

            // Create a list to hold the view models for all products
            List<FoodItemsViewModel> fooditemsViewModels = new List<FoodItemsViewModel>();

            // Loop through each product and create a view model for it
            foreach (var fooditem in fooditems)
            {
                FoodItemsViewModel fooditemsModel = new FoodItemsViewModel()
                {
                    ImageUrl = fooditem.ImageUrl,
                    ItemName = fooditem.ItemName,
                    Cost = fooditem.Cost,
                    Rating = fooditem.Rating,
                    ItemDescription = fooditem.ItemDescription,
                    Id = fooditem.Id,
                };

                // Add the view model to the list
                fooditemsViewModels.Add(fooditemsModel);
            }

            // Pass the list of view models to the view
            return View(fooditemsViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int fooditemid)
        {
            TempData["NoProductFound"] = null;

            // Retrieve the product with the specified id from the database
            var fooditem = await _context.tblFoodItems.FirstOrDefaultAsync(p => p.Id == fooditemid);

            // Check if product is null
            if (fooditem == null)
            {
                TempData["NoProductFound"] = $"Unable to find the Food item details for ID : {fooditemid}";
                return View(TempData["NoProductFound"]);
            }

            FoodItemsViewModel fooditemmodel = new FoodItemsViewModel()
            {
                ImageUrl = fooditem!.ImageUrl,
                ItemName = fooditem.ItemName,
                Cost = fooditem.Cost,
                Rating = fooditem.Rating,
                Id = fooditem.Id,
            };

            // Pass the product to the view for rendering
            return View(fooditemmodel);
        }

        [HttpGet]
        public async Task<IActionResult> GetFoodItemsByCategory(string category)
        {
            if (!string.IsNullOrWhiteSpace(category))
            {
                var categoryEnum = (FoodItemsEntity.FoodItemCategory)Enum.Parse(typeof(FoodItemsEntity.FoodItemCategory), category, true);

                var fooditems = await _context.tblFoodItems
                    .Where(p => p.FoodItemCategoryId == categoryEnum)
                    .ToListAsync();

                if (fooditems.Count == 0)
                {
                    TempData["NoProducts"] = $"No products available for category: {category}";
                    return RedirectToAction("Index");
                }

                var fooditemviewmodels = fooditems.Select(fooditem => new FoodItemsViewModel
                {
                    ImageUrl = fooditem.ImageUrl,
                    ItemName = fooditem.ItemName,
                    Cost = fooditem.Cost,
                    Rating = fooditem.Rating,
                    Id = fooditem.Id
                }).ToList();

                return View("Index", fooditemviewmodels);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            var results = await _context.tblFoodItems
                .Where(p => p.ItemName.Contains(query))
                .Select(p => new { p.ItemName })
                .ToListAsync();

            return Json(results);
        }
    }
}
