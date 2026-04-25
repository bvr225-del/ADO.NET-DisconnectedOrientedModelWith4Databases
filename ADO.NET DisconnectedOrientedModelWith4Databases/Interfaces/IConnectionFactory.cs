using Microsoft.Data.SqlClient;

namespace ADO.NET_DisconnectedOrientedModelWith4Databases.Interfaces
{
    public interface IConnectionFactory
    {

        //WE NEED TO CREATE A METHOD FOR EACH DATABASE CONNECTION STRING IN THE CONNECTION FACTORY
        //INTERFACE AND IMPLEMENT IT IN THE CONNECTION FACTORY CLASS.
        SqlConnection Hotelmanagement1SqlConnectionString();
        SqlConnection Midland1SqlConnectionString();
        SqlConnection Nortwind_DB1SqlConnectionString();
    }
}
