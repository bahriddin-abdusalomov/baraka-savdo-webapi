using Baraka_Savdo.DataAccess.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraka_Savdo.DataAccess.Interfaces.Categories
{
    public interface ICotegoryRepository : IRepository<Category, Category>, IGetAll<Cotegory>
    {
    }
}
