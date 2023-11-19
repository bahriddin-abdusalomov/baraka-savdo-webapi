namespace Baraka_Savdo.DataAccess.Interfaces.Products;

public interface IProductRepository : IRepository<Product, ProductViewModel>,
                                        IGetAll<ProductViewModel>, ISearchable<ProductViewModel>
{
}
