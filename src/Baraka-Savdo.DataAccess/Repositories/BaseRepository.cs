using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraka_Savdo.DataAccess.Repositories
{
    public class BaseRepository
    {
        protected readonly NpgsqlConnection _connection;
        public BaseRepository() 
        { 
            this._connection = new NpgsqlConnection("server = localhost; port = 5432; " +
                "user id = postgres; password = salom; database = baraka-savdo-db;");
        }
    }
}
