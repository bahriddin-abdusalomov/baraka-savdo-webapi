using Baraka_Savdo.DataAccess.Common.Interfaces;
using Baraka_Savdo.DataAccess.ViewModels.Users;
using Baraka_Savdo.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraka_Savdo.DataAccess.Interfaces.Users
{
    public interface IUserRepository : IRepository<User, UserViewModel>,
          IGetAll<UserViewModel>, ISearchable<UserViewModel>
    {
        public Task<User?> GetByPhoneAsync(string phone);
    }
}
