namespace Baraka_Savdo.Service.Interfaces.Categories;

public interface ICategoryService
{
    public Task<bool> CreateAsync(CategoryCreateDto dto);

    public Task<bool> DeleteAsync(long categoryId);

    public Task<long> CountAsync();

    public Task<Category> GetByIdAsync(long categoryId);
    
    public Task<IList<Category>> GetAllAsync(PaginationParams @params);

    public Task<bool> UpdateAsync(long categoryId, CategoryUpdateDto dto);
}
