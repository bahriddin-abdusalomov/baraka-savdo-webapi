using Baraka_Savdo.DataAccess.Common.Interfaces;
using Baraka_Savdo.DataAccess.ViewModels.Products;
using Baraka_Savdo.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraka_Savdo.DataAccess.Interfaces.Products
{
    public interface IProductRepository : IRepository<Product, ProductViewModel>, IGetAll<ProductViewModel>, ISearchable<ProductViewModel>
    {
    }
}
