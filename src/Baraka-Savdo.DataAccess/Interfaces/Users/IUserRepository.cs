namespace Baraka_Savdo.DataAccess.Interfaces.Users;

public interface IUserRepository : IRepository<User>,
      IGetAll<User>, ISearchable<User>
{
    public Task<IList<User>?> GetByPhoneAsync(string phone);
}
