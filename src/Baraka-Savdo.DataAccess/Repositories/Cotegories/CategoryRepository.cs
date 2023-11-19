using Baraka_Savdo.DataAccess.Interfaces.Categories;
using Baraka_Savdo.DataAccess.Utils;
using Baraka_Savdo.Domain.Entities.Categories;
using Dapper;
using static Dapper.SqlMapper;

namespace Baraka_Savdo.DataAccess.Repositories.Cotegories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public async Task<long> CountAsync()
        {
            try
            {
                await _connection.OpenAsync();
                string query = "Select count(*) From categories";
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

        public async Task<int> CreateAsync(Category entity)
        {
            try
            {
                await _connection.OpenAsync();

                string query = "INSERT INTO public.categories(name, description, image_path, created_at, updated_at)" +
                    " VALUES (@Name, @Description, @ImagePath, @CreatedAt, @UpdatedAt);";
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

                string query = "Delete From categories Where id = @Id";
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

        public async Task<IList<Category>> GetAllAsync(PaginationParams @params)
        {
            try
            {
                await _connection.OpenAsync();

                string query = $"Select * From categories order by id desc  " +
                    $"offset {@params.GetSkipCount()} limit {@params.PageSize} ";

                var result = (await _connection.QueryAsync<Category>(query)).ToList();
                return result;

            }
            catch
            {   
                return new List<Category>();
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<Category?> GetByIdAsync(long id)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "Select * From categories Where id = @Id";
                var result = await _connection.QuerySingleAsync<Category>(query, new { Id = id });
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

        public async Task<int> UpdateAsync(long id, Category entity)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"UPDATE public.categories " +
                    $"SET name=@Name, description=@Description, image_path=@ImagePath, created_at=@CreatedAt, updated_at=@UpdatedAt " +
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
}
