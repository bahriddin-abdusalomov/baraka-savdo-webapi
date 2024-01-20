namespace Baraka_Savdo.DataAccess.Repositories.Products;

public class ProductRepository : BaseRepository, IProductRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();

            string query = "Select count(*) From products";
            var result = await _connection.QuerySingleAsync<long>(query);

            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> CreateAsync(Product entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "INSERT INTO public.products(name, description, image_path, unit_price, category_id, company_id, created_at, updated_at)" +
                " VALUES (@Name, @Description, @ImagePath, @UnitPrice, @CategoryId, @CompanyId, @CreatedAt, @UpdatedAt);";
            var result = await _connection.ExecuteAsync(query, entity);
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> DeleteAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "Delete From products Where id = @Id";
            var result = await _connection.ExecuteAsync(query, new { Id = id });
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<IList<Product>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"Select * From products order by id asc  " +
                $"offset {@params.GetSkipCount()} limit {@params.PageSize} ";

            var result = (await _connection.QueryAsync<Product>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<Product>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<Product?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "Select * From products Where id = @Id";
            var result = await _connection.QuerySingleAsync<Product>(query, new { Id = id });
            return result;
        }
        catch
        {
            return null;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<(int count, List<Product>)> SearchAsync(string search, PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "SELECT * " +
               "FROM products " +
               "WHERE name LIKE '%' + @searchTerm + '%';";

            var result = (await _connection.QueryAsync<Product>(query, new { searchTerm = search })).ToList();

            return (@params.PageNumber, result);
        }
        catch
        {
            return (@params.PageNumber, new List<Product>());
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> UpdateAsync(long id, Product entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"UPDATE public.products " +
                $"SET name=@Name, description=@Description, image_path=@ImagePath, unit_price=@UnitPrice, category_id=@CategoryId, company_id=@CompanyId, created_at=@CreatedAt, updated_at=@UpdatedAt " +
                $"WHERE id={id};";

            var result = await _connection.ExecuteAsync(query, entity);
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }
}
