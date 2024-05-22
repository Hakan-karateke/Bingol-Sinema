using Microsoft.EntityFrameworkCore;
using BingolSinema.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure the database context
var connectionString = builder.Configuration.GetConnectionString("BingolSinemaDB");
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<MyDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Giris}/{id?}");

app.Run();
