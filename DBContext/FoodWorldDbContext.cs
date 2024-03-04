using Food_World.Models.EFDBContext;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Food_World.DBContext
{
    /// <summary>
    /// The entity framework DB context class.
    /// </summary>
    public class FoodWorldDbContext : IdentityDbContext<UserRegistrationContext>
    {
        private readonly IConfiguration configuration;

        public FoodWorldDbContext(DbContextOptions<FoodWorldDbContext> options, IConfiguration configuration)
            : base(options)
        {
            this.configuration = configuration;
        }

        public virtual DbSet<UserRegistrationContext> AspNetUsers { get; set; } = null!;

        public virtual DbSet<FoodItemsEntity> tblFoodItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
