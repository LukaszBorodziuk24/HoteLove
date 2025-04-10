using HoteLove;
using HoteLove.Models;
using HoteLove.Services;
using HoteLove.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DbHoteLoveContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Hotelove");
    options.UseSqlServer(connectionString, builder =>
    {
        builder.EnableRetryOnFailure();
    });
});
builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DbHoteLoveContext>();

builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<IUserContext, UserContext>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<IManagementService, ManagementService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var context = scope.ServiceProvider.GetRequiredService<DbHoteLoveContext>();

    // Inicjalizacja ról
    if (!roleManager.RoleExistsAsync("Regular_User").Result)
    {
        IdentityRole role = new IdentityRole("Regular_User");
        roleManager.CreateAsync(role).Wait();
    }

    if (!roleManager.RoleExistsAsync("Hotel_User").Result)
    {
        IdentityRole role = new IdentityRole("Hotel_User");
        roleManager.CreateAsync(role).Wait();
    }
}

app.Run();