﻿using Baraka_Savdo.DataAccess.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraka_Savdo.DataAccess.Interfaces.Users
{
    public interface IUserRepository : IRepository<User, UserViewModel>, IGetAll<UserViewModel>, ISearchable<UserViewModel>
    {
        public Task<User?> GetByPhoneAsync(string phoneNumber);
    }
}
