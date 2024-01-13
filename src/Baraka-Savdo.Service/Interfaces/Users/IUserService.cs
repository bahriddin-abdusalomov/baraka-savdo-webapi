using Baraka_Savdo.Domain.Entities.Users;

namespace Baraka_Savdo.Service.Interfaces.Users;

public interface IUserService
{
    public Task<bool> CreateAsync(UserCreateDto dto);

    public Task<bool> DeleteAsync(long UserId);

    public Task<long> CountAsync();

    public Task<User> GetByIdAsync(long UserId);
  
    public Task<User> GetByPhoneAsync(string Name);
    
    public Task<IList<User>> GetAllAsync(PaginationParams @params);

    public Task<(int TModel, List<User>)> SearchAsync(string search, PaginationParams @params);

    public Task<bool> UpdateAsync(long UserId, UserUpdateDto dto);

}
