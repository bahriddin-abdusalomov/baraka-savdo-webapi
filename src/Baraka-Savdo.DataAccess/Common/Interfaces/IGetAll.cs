using Baraka_Savdo.DataAccess.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraka_Savdo.DataAccess.Common.Interfaces
{
    public interface IGetAll<TModel>
    {
        public Task<TModel> GetAllAsync(PaginationParams @params);
    }
}
