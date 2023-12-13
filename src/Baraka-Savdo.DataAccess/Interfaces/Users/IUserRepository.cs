namespace Baraka_Savdo.DataAccess.Interfaces.Users;

public interface IUserRepository : IRepository<User, UserViewModel>,
      IGetAll<UserViewModel>, ISearchable<UserViewModel>
{
    public Task<IList<UserViewModel>?> GetByPhoneAsync(string phone);
}
