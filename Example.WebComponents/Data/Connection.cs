using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;

namespace Example.WebComponents.Data
{
    public class Connection
    {
        private readonly string connectionString = "Data Source=DESKTOP-GVLTI9D\\SQLEXPRESS;Initial Catalog=Example;Encrypt=False;Integrated Security=True";

        public SqlConnection GetSqlConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
