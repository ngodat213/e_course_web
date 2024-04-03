using e_course_web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using e_course_web.Repositorys;

var builder = WebApplication.CreateBuilder(args);

// Add database sql server
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register Repository for using in controller
builder.Services.AddRazorPages();

// Add IUnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// App build
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

/*app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");*/

app.UseEndpoints(endpoints =>{
    endpoints.MapRazorPages(); 
    endpoints.MapControllerRoute("default", "{area=Customer}/{controller=Home}/{action=Index}/{id?}"); 
});

app.Run();
