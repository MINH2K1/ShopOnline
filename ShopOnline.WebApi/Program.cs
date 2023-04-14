using Microsoft.EntityFrameworkCore;
using ShopOnline.Application.Command.Products;
using ShopOnline.Data.Data_Context;
using ShopOnline.Data.Extention;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();

builder.Services.AddDbContext<ShopOnline_Context>(options => options.UseSqlServer(configuration.GetConnectionString("ShopOnline")));
builder.Services.AddTransient<IProductService, PublicProductService>();
builder.Services.AddTransient<IProductManagementService, ManagementProductService>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
