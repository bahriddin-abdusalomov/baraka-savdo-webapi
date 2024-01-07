namespace Baraka_Savdo.DataAccess.Common.Interfaces;

public interface ISearchable<TModel>
{
    public Task<(int count, List<TModel>)> SearchAsync(string search, PaginationParams @params);
}
