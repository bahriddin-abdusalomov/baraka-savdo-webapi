using Baraka_Savdo.DataAccess.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraka_Savdo.DataAccess.Interfaces
{
    public interface IDiscountRepository : IRepository<Discount, Discount>, IGetAll<Discount>
    {
    }
}
