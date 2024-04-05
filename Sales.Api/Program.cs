using Microsoft.EntityFrameworkCore;
using Sales.Application.Contract;
using Sales.Application.Service;
using Sales.Infraestructure.Context;
using Sales.Infraestructure.Interfaces;
using Sales.Infraestructure.Repositories;
using Sales.Ioc.CategoryDependecy;
using Sales.Ioc.TDocumentDependency;


var builder = WebApplication.CreateBuilder(args);

// **Agrega el contexto de la base de datos aquí:**
builder.Services.AddDbContext<SalesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SalesContext")));


builder.Services.AddCategoryDependency();

builder.Services.AddTDocumentDependecy();

// Repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IProductoRepository, ProductRepository>();

builder.Services.AddScoped<ITipoDocumentoVentaRepository, TipoDocumentoVentaRepository>();


//App services

builder.Services.AddTransient<ICategoryService, CategoryNewService>();

builder.Services.AddTransient<IProductService, ProductService>();

builder.Services.AddTransient<ITDocumentVentService, TDocumentServiceNew>();

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
