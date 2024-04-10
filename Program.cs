using e_course_web.DataAccess.Repositorys;
using Microsoft.EntityFrameworkCore;
using e_course_web.DataAccess.DbContext;
using e_course_web.Service.Helpers;
using e_course_web.Service.Interfaces;
using e_course_web.Service.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using e_course_web.Models;

var builder = WebApplication.CreateBuilder(args);

// Add database sql server
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register service for page
builder.Services.AddRazorPages();

// Add default Identity
builder.Services.AddIdentity<User, IdentityRole>()
.AddDefaultTokenProviders()
.AddDefaultUI()
.AddEntityFrameworkStores<ApplicationDbContext>();

// Adds a AddScoped IUnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// Adds a AddScoped ICloudinaryService
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();

// Giới hạn kích thước tối đa của phần thân của yêu cầu HTTP mà máy chủ Kestrel có thể chấp nhận
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 512 * 1024 * 1024;
});

// Register configure CloudinarySettings
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
// Limit file is 512MB
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 512 * 1024 * 1024;
});

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

app.UseEndpoints(endpoints =>{
    endpoints.MapRazorPages(); 
    endpoints.MapControllerRoute("default", "{area=Customer}/{controller=Home}/{action=Index}/{id?}"); 
});

app.Run();
