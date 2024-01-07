using Baraka_Savdo.DataAccess.Interfaces.Products;
using Baraka_Savdo.Domain.Entities.Products;
using Baraka_Savdo.Domain.Exceptions.Files;
using Baraka_Savdo.Domain.Exceptions.Products;
using Baraka_Savdo.Service.Common.Helpers;
using Baraka_Savdo.Service.Dtos.Products;
using Baraka_Savdo.Service.Interfaces.Products;

namespace Baraka_Savdo.Service.Services.Products;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IFileService _fileService;
    private readonly IPaginator _paginator;

    public ProductService(
        IProductRepository productRepository, 
        IFileService fileService, 
        IPaginator paginator)
    {
        _productRepository = productRepository;
        _fileService = fileService;
        _paginator = paginator;
    }

    public async Task<long> CountAsync()
    {
        long count = await _productRepository.CountAsync();
        return count;
    }

    public async Task<bool> CreateAsync(ProductCreateDto dto)
    {
        string imagePath = await _fileService.UploadImageAsync(dto.ImagePath);
        Product product = new Product()
        {
            Name = dto.Name,
            ImagePath = imagePath,
            Description = dto.Description,
            UnitPrice = dto.UnitPrice,
            CategoryId = dto.CategoryId,
            CompanyId = dto.CompanyId,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };

        var result = await _productRepository.CreateAsync(product);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(long productId)
    {
        var user = await _productRepository.GetByIdAsync(productId);
        if (user == null) throw new ProductNotFoundException();

        var userImage = await _fileService.DeleteImageAsync(user.ImagePath);
        if (userImage == false) throw new ImageNotFoundException();

        var result = await _productRepository.DeleteAsync(productId);
        return result > 0;
    }

    public async Task<IList<Product>> GetAllAsync(PaginationParams @params)
    {
        var products = await _productRepository.GetAllAsync(@params);
        var count = await _productRepository.CountAsync();
        _paginator.Paginate(count, @params);
        return products;
    }

    public async Task<Product> GetByIdAsync(long productId)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null) throw new ProductNotFoundException();

        return product;
    }

    public async Task<(int count, List<Product>)> SearchAsync(string search, PaginationParams @params)
    {
        var product = await _productRepository.SearchAsync(search, @params);
        if (product.count == 0) throw new ProductNotFoundException();
        return product;
    }

    public async Task<bool> UpdateAsync(long productId, ProductUpdateDto dto)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product is null) throw new ProductNotFoundException();

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.UnitPrice = dto.UnitPrice;
        product.CategoryId = dto.CategoryId;
        product.CompanyId = dto.CompanyId;


        if (dto.ImagePath is not null)
        {
            var deleteResult = await _fileService.DeleteImageAsync(product.ImagePath);
            if (deleteResult is false) throw new ImageNotFoundException();

            string newImagePath = await _fileService.UploadImageAsync(dto.ImagePath);
            product.ImagePath = newImagePath;
        }
        product.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _productRepository.UpdateAsync(productId, product);
        return dbResult > 0;
    }
}
