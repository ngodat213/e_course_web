using e_course_web.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register Repository for using in controller
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddRazorPages();

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
