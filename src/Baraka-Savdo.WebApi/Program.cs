using Baraka_Savdo.DataAccess.Interfaces.Categories;
using Baraka_Savdo.DataAccess.Repositories.Cotegories;
using Baraka_Savdo.Service.Interfaces.Categories;
using Baraka_Savdo.Service.Interfaces.Common;
using Baraka_Savdo.Service.Services.Catecories;
using Baraka_Savdo.Service.Services.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IFileService, FileService>();

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
