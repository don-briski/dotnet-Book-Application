using Bulky.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(
               options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
// builder.Services.AddDbContext<ApplicationDbContext>(option => 
//     option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
//     sqlServerOptionsAction: sqlOptions => {
//         sqlOptions.EnableRetryOnFailure(
//     //         maxRetryCount: 5,
//     //         maxRetryDelay: TimeSpan.FromSeconds(30),
//     //         errorNumbersToAdd: null
//     );
//      })
//     );
    
    //  internal void ConfigureServices(IServiceCollection services)
    // {
    //     services.AddControllersWithViews();
    //     services.AddDbContext<ApplicationDbContext>(option => 
    //         option.UseSqlServer(IConfiguration.GetConnectionString("DefaultConnection"),
    //         sqlServerOptionsAction: sqlOptions => {
    //             sqlOptions.EnableRetryOnFailure(
    //                 maxRetryCount: 5,
    //                 maxRetryDelay: TimeSpan.FromSeconds(30),
    //                 errorNumbersToAdd: null
    //             );
    //         })
    //     );
    // }

var app = builder.Build();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
