using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraka_Savdo.DataAccess.Interfaces
{
    public interface IRepository<TEntity, TViewModel>
    {
        public Task<int> CreateAsync(TEntity entity);

        public Task<int> UpdateAsync(long id, TEntity entity);

        public Task<int> DeleteAsync(long id);

        public Task<TViewModel?> GetByIdAsync(long id);

        public Task<long> CountAsync();
    }
}
