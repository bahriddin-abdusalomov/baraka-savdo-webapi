using Baraka_Savdo.Domain.Entities.Products;
using Baraka_Savdo.Domain.Entities.Users;
using Baraka_Savdo.Service.Dtos.Products;

namespace Baraka_Savdo.Service.Interfaces.Products;

public interface IProductService
{
    public Task<bool> CreateAsync(ProductCreateDto dto);

    public Task<bool> DeleteAsync(long productId);

    public Task<long> CountAsync();

    public Task<Product> GetByIdAsync(long productId);

    public Task<IList<Product>> GetAllAsync(PaginationParams @params);

    public Task<(int count, List<Product>)> SearchAsync(string search, PaginationParams @params);

    public Task<bool> UpdateAsync(long productId, ProductUpdateDto dto);

}
