using Microsoft.Extensions.Configuration;

namespace Baraka_Savdo.DataAccess.Repositories;

public class BaseRepository
{
    protected readonly NpgsqlConnection _connection;
    public BaseRepository() 
    { 
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        var connectionString = "server = localhost; port = 5432; user id = postgres; password = salom; database = savdo-db;";
        _connection = new NpgsqlConnection(connectionString);
    }
}
