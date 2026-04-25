using ADO.NET_DisconnectedOrientedModelWith4Databases.Interfaces;
using ADO.NET_DisconnectedOrientedModelWith4Databases.Utils;
using Microsoft.Data.SqlClient;

namespace ADO.NET_DisconnectedOrientedModelWith4Databases.Data
{
    public class ConnectionFactory : IConnectionFactory
    {
        /*
          IF YOU WANT READ THE CONNECTION STRING FROM APPSETTINGS.JSON FILE,
          WE HAVE ONE PREDEFINE INTERFACE IS AVAILABLE IN .NET CALLED IConfiguration, 
          WE CAN INJECT IT IN THE CONSTRUCTOR OF THE CONNECTION FACTORY CLASS AND THEN READ THE CONNECTION STRING FROM APPSETTINGS.JSON FILE.
         */

        private readonly IConfiguration _configuration;
        public ConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public SqlConnection Hotelmanagement1SqlConnectionString()
        {
            var connectionString = Convert.ToString(_configuration.GetSection(ConnectionStringNames.Hotelmanagement1_DBConnectionstringname).Value);
            SqlConnection con = new SqlConnection(connectionString);
            return con;
        }
        public SqlConnection Midland1SqlConnectionString()
        {
            var connectionString = Convert.ToString(_configuration.GetSection(ConnectionStringNames.Midland1_DBConnectionstringname).Value);
            SqlConnection con = new SqlConnection(connectionString);
            return con;
        }

        public  SqlConnection Nortwind_DB1SqlConnectionString()
        {
            var connectionString = Convert.ToString(_configuration.GetSection(ConnectionStringNames.Northwind_DB1Connectionstringname).Value);
            SqlConnection con = new SqlConnection(connectionString);
            return con;
        }
    }
}