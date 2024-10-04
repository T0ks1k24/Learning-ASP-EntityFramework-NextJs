using Entity_Framework.Data;
using Entity_Framework.Repositories.Categories;
using Entity_Framework.Repositories.Customers;
using Entity_Framework.Repositories.OrderItems;
using Entity_Framework.Repositories.Orders;
using Entity_Framework.Repositories.Products;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Connect Database
builder.Services.AddDbContext<LearningDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("LearningDbContext")));

builder.Services.AddScoped<IProductReposity, ProductReposity>();
builder.Services.AddScoped<IOrderReposity, OrderReposity>();
builder.Services.AddScoped<ICategoryReposity, CategoryReposity>();
builder.Services.AddScoped<IOrderItemReposity, OrderItemReposity>();
builder.Services.AddScoped<ICustomerReposity, CustomerReposity>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
