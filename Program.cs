using Food_World.DBContext;
using Food_World.Models.EFDBContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add ApplicationDbContext to the services
builder.Services.AddDbContext<FoodWorldDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Food_World_DB")));

// Add Identity services
builder.Services.AddIdentity<UserRegistrationContext, IdentityRole>()
    .AddEntityFrameworkStores<FoodWorldDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
