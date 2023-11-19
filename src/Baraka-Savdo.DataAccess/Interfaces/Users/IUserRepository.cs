namespace Baraka_Savdo.DataAccess.Interfaces.Users;

public interface IUserRepository : IRepository<User, UserViewModel>,
      IGetAll<UserViewModel>, ISearchable<UserViewModel>
{
    public Task<User?> GetByPhoneAsync(string phone);
}
