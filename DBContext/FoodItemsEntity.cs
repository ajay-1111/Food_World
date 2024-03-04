namespace Food_World.DBContext
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

        public string? ItemName { get; set; }

        public double Cost { get; set; }

        public double Rating { get; set; }

        public string? ItemDescription { get; set; }

        public string? ImageUrl { get; set; }

        public FoodItemCategory FoodItemCategoryId { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime ModifiedDateTime { get; set; }
    }
}
