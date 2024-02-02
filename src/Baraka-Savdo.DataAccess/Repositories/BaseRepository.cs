namespace Baraka_Savdo.DataAccess.Repositories;

public class BaseRepository
{
    protected readonly NpgsqlConnection _connection;
    public BaseRepository() 
    { 
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        this._connection = new NpgsqlConnection("server = localhost; port = 5432; " +
            "user id = postgres; password = salom; database = savdo-db;");
    }
}
