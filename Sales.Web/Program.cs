using Sales.Application.Contract;
using Sales.Application.Service;
using Sales.Web.Services.Contract;
using Sales.Web.Services.ServicesEntities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddHttpClient();

builder.Services.AddTransient<ICategoryServices, CategoryServices>();
builder.Services.AddHttpClient<ICategoryServices, CategoryServices>();

builder.Services.AddTransient<IProductServices, ProductServices>();
builder.Services.AddHttpClient<IProductServices, ProductServices>();

builder.Services.AddTransient<ITdocumentVentaSevices, TDocuementServices>();
builder.Services.AddHttpClient<ITdocumentVentaSevices, TDocuementServices>();

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
