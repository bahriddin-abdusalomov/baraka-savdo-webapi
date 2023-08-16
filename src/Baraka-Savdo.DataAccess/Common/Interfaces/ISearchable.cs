using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraka_Savdo.DataAccess.Common.Interfaces
{
    public interface ISearchable<TModel>
    {
        public Task<(int TModel, List<TModel>)> SearchAsync(string search, PaginationParams @params);
    }
}
