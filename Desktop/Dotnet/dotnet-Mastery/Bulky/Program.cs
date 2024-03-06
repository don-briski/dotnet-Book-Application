using Bulky;
using Bulky.DataAccess.Data;
using Bulky.Repository;
using Bulky.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");



builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)
   // .EnableSensitiveDataLogging()
    );
// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUnitofWork, UnitofWork>();

// builder.Services.AddDbContext<ApplicationDbContext>(
//                options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

 //   Console.WriteLine($"Connection String: {builder.Configuration.GetConnectionString("DefaultConnection")}");


var app = builder.Build();
//var options = app.Services.GetRequiredService<DbContextOptions<ApplicationDbContext>>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
} else {
     app.Urls.Clear();
    app.Urls.Add("http://localhost:5000");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
