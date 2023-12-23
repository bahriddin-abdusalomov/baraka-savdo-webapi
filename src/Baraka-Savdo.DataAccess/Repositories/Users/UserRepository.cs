using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics.Metrics;

namespace Baraka_Savdo.DataAccess.Repositories.Users;

public class UserRepository : BaseRepository, IUserRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();

            string query = "Select count(*) From users";
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
    public async Task<int> CreateAsync(User entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "INSERT INTO public.users(" +
                            "first_name, last_name, " +
                            "is_male, birth_date, country, region, " +
                            "image_path, phone_number, phone_number_confirmed, passport_seria_number, password_hash, salt," +
                            "last_activity, created_at, updated_at) " +
                            "VALUES (@FirstName, @LastName, " +
                            "@IsMale, @BirthDate, @Country, @Region, @ImagePath,   @PhoneNumber, @PhoneNumberConfirmed," +
                            "@PassportSeriaNumber,  @PasswordHash, @Salt," +
                            "@LastActivity, @CreatedAt, @UpdatedAt);";
           
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

            string query = "Delete From users Where id = @Id";
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

    public async Task<IList<User>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"Select * From users order by id desc  " +
                $"offset {@params.GetSkipCount()} limit {@params.PageSize} ";

            var result = (await _connection.QueryAsync<User>(query)).ToList();
            return result;

        }
        catch
        {
            return new List<User>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<User?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "Select * From users Where id = @Id";
            var result = await _connection.QuerySingleAsync<User>(query, new { Id = id });
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

    public async Task<IList<User>?> GetByPhoneAsync(string phone)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "SELECT * " +
               "FROM users " +
               "WHERE phone_number LIKE '%' + @searchPhoneNumber + '%';";

            var result = (await _connection.QueryAsync<User>(query, new { searchPhoneNumber = phone })).ToList();

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

    public async Task<(int TModel, List<User>)> SearchAsync(string search, PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "SELECT * " +
               "FROM users " +
               "WHERE " +
               "    first_name LIKE '%' + @searchTerm + '%' OR " +
               "    last_name LIKE '%' + @searchTerm + '%' OR " +
               "    phone_number LIKE '%' + @searchTerm + '%' OR " +
               "    passport_seria_number LIKE '%' + @searchTerm + '%' OR " +
               "    passport_seria_confirmed LIKE '%' + @searchTerm + '%' OR " +
               "    birth_date LIKE '%' + @searchTerm + '%' OR " +
               "    is_male LIKE '%' + @searchTerm + '%' OR " +
               "    country LIKE '%' + @searchTerm + '%' OR " +
               "    region LIKE '%' + @searchTerm + '%';";

            var result = (await _connection.QueryAsync<User>(query, new { searchTerm = search })).ToList();

            return (@params.PageNumber, result);
        }
        catch
        {
            return (@params.PageNumber, new List<User>());
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> UpdateAsync(long id, User entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "UPDATE public.users " +
                     "SET first_name = @FirstName, " +
                     "    last_name = @LastName, " +
                     "    phone_number = @PhoneNumber, " +
                     "    phone_number_confirmed = @PhoneNumberConfirmed, " +
                     "    passport_seria_number = @PassportSeriaNumber, " +
                     "    is_male = @IsMale, " +
                     "    birth_date = @BirthDate, " +
                     "    country = @Country, " +
                     "    region = @Region, " +
                     "    password_hash = @PasswordHash, " +
                     "    salt = @Salt, " +
                     "    image_path = @ImagePath, " +
                     "    last_activity = @LastActivity, " +
                     "    identity_role = @IdentityRole, " +
                     "    updated_at = @UpdatedAt " +
                     "WHERE id = @Id;";

            entity.Id = id;
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
