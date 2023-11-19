namespace Baraka_Savdo.DataAccess.Common.Interfaces;

public interface IGetAll<TModel>
{
    public Task<IList<TModel>> GetAllAsync(PaginationParams @params);
}
