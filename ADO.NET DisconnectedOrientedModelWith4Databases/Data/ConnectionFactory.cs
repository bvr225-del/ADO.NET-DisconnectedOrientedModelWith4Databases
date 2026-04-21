using ADO.NET_DisconnectedOrientedModelWith4Databases.Interfaces;
using Microsoft.Data.SqlClient;

namespace ADO.NET_DisconnectedOrientedModelWith4Databases.Data
{
    public class ConnectionFactory:IConnectionFactory
    {
        /*
          IF YOU WANT READ THE CONNECTION STRING FROM APPSETTINGS.JSON FILE,
          WE HAVE ONE PREDEFINE INTERFACE IS AVAILABLE IN .NET CALLED IConfiguration, 
          WE CAN INJECT IT IN THE CONSTRUCTOR OF THE CONNECTION FACTORY CLASS AND THEN READ THE CONNECTION STRING FROM APPSETTINGS.JSON FILE.
         */

        private readonly IConfiguration _configuration;
        public ConnectionFactory(IConfiguration configuration)
        {
            _configuration= configuration;
        }
        public SqlConnection Hotelmanagement1SqlConnectionString()
        {
            var connectionString=Convert.ToString(_configuration.GetSection("ConnectionStrings:Hotelmanagement1SqlConnectionString").Value);
            SqlConnection con=new SqlConnection(connectionString);
            return con;
        }

    }
}
