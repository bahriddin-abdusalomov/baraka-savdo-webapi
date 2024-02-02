namespace Baraka_Savdo.DataAccess.Interfaces.Users;

public interface IUserRepository : IRepository<User>,
      IGetAll<User>, ISearchable<User>
{
    public Task<User> GetByEmailAsync(string email);
}
