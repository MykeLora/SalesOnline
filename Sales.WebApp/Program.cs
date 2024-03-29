using Microsoft.EntityFrameworkCore;
using Sales.Infraestructure.Context;
using Sales.Ioc.CategoryDependecy;
using Sales.Ioc.ProductDependecy;

var builder = WebApplication.CreateBuilder(args);

// **Agrega el contexto de la base de datos aquí:**
builder.Services.AddDbContext<SalesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SalesContext")));

//App services
builder.Services.AddCategoryDependency();

builder.Services.AddProductDependecy();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
