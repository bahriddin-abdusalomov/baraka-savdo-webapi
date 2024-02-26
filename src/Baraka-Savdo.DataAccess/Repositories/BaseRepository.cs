using Microsoft.Extensions.Configuration;

namespace Baraka_Savdo.DataAccess.Repositories;

public class BaseRepository
{
    protected readonly NpgsqlConnection _connection;
    public BaseRepository(IConfiguration configuration) 
    { 
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        _connection = new NpgsqlConnection(connectionString);
    }
}
