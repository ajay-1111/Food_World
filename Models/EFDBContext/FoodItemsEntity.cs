using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Food_World.Models.EFDBContext
{
    public class FoodItemsEntity
    {
        public enum FoodItemCategory
        {
            Burger = 1,
            Pizza = 2,
            Dessert = 3,
        }
        public int Id { get; set; }

        [DisplayName("Item Name")]
        public string ItemName { get; set; }

        public double Cost { get; set; }

        public double Rating { get; set; }

        public string ItemDescription { get; set; }

        [DisplayName("Image")]
        public string ImageUrl { get; set; }

        [DisplayName("Food Item Category")]
        public FoodItemCategory FoodItemCategoryId { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime ModifiedDateTime { get; set; }
    }
}
