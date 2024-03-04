namespace Food_World.Models
{
    public class FoodItemsViewModel
    {
        public int Id { get; set; }

        public string? ItemName { get; set; }

        public double Cost { get; set; }

        public double Rating { get; set; }

        public string? ImageUrl { get; set; }

        public string? ItemDescription { get; set; }
    }
}
