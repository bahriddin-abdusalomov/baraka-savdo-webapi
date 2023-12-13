namespace Baraka_Savdo.DataAccess.Interfaces.Products;

public interface IProductRepository : IRepository<Product>,
                                        IGetAll<Product>, ISearchable<Product>
{
}
