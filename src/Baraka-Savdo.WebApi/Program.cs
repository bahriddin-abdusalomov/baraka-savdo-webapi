using Baraka_Savdo.DataAccess.Interfaces.Categories;
using Baraka_Savdo.DataAccess.Interfaces.Companies;
using Baraka_Savdo.DataAccess.Interfaces.Products;
using Baraka_Savdo.DataAccess.Interfaces.Users;
using Baraka_Savdo.DataAccess.Repositories.Companies;
using Baraka_Savdo.DataAccess.Repositories.Cotegories;
using Baraka_Savdo.DataAccess.Repositories.Products;
using Baraka_Savdo.DataAccess.Repositories.Users;
using Baraka_Savdo.Service.Interfaces.Auth;
using Baraka_Savdo.Service.Interfaces.Categories;
using Baraka_Savdo.Service.Interfaces.Common;
using Baraka_Savdo.Service.Interfaces.Companies;
using Baraka_Savdo.Service.Interfaces.Products;
using Baraka_Savdo.Service.Interfaces.Users;
using Baraka_Savdo.Service.Services.Auth;
using Baraka_Savdo.Service.Services.Catecories;
using Baraka_Savdo.Service.Services.Common;
using Baraka_Savdo.Service.Services.Companies;
using Baraka_Savdo.Service.Services.Products;
using Baraka_Savdo.Service.Services.Users;
using Baraka_Savdo.WebApi.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:4200", "http://185.217.131.187/")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.SwaggerConfigure();
builder.JWTConfigure();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IPaginator, Paginator>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
