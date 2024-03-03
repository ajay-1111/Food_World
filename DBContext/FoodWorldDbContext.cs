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

        /// <summary>
        /// Initializes a new instance of the <see cref="FoodWorldDbContext"/> class.
        /// The FoodWorldDbContext constructor.
        /// </summary>
        /// <param name="options">The Option.</param>
        /// <param name="configuration">The configuration from appsettings.json.</param>
        public FoodWorldDbContext(DbContextOptions<FoodWorldDbContext> options, IConfiguration configuration)
            : base(options)
        {
            this.configuration = configuration;
        }

        /// <summary>Gets or sets the Registration Table.</summary>
        public virtual DbSet<UserRegistrationContext> TblRegisterUser { get; set; } = null!;

        /// <summary>
        /// The on model creation after DB Mifration.
        /// </summary>
        /// <param name="builder">The builder.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
