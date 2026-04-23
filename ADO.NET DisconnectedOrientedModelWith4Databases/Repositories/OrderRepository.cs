using ADO.NET_DisconnectedOrientedModelWith4Databases.Interfaces;
using ADO.NET_DisconnectedOrientedModelWith4Databases.Models;
using ADO.NET_DisconnectedOrientedModelWith4Databases.Utils;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
namespace ADO.NET_DisconnectedOrientedModelWith4Databases.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public OrderRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory= connectionFactory;
        }
        public async Task<int> AddOrder(Orders orderdetail)
        {
            using(SqlConnection con=_connectionFactory.Midland1SqlConnectionString())
            {
                SqlCommand cmd = new SqlCommand(StoredProcedures.AddOrder, con);
                cmd.CommandType=CommandType.StoredProcedure;
                //pass the data to input partameters of your storedprocedure
                cmd.Parameters.AddWithValue(StoredProcedureParameters.orderName, orderdetail.orderName);
                cmd.Parameters.AddWithValue(StoredProcedureParameters.orderLocation, orderdetail.orderLocation);

                //below code is used to store the stoedprocedure return value.
                SqlParameter outputParam = new SqlParameter(StoredProcedureParameters.Insertedvariable, SqlDbType.Int);
                outputParam.Direction = ParameterDirection.Output;//if stooredprocedure returns any output params value.by using this process we can return
                cmd.Parameters.Add(outputParam);//need to add output parameter to sqlcommand object.this is the rule.
                SqlDataAdapter da= new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds,ClassNames.Orders);
                var ordersCount = (int)cmd.Parameters[StoredProcedureParameters.insertedVariable].Value;
                return ordersCount;
            }

        }

        public async Task<bool> DeleteOrder(int orderId)
        {
            using(SqlConnection con=_connectionFactory.Midland1SqlConnectionString())
            {
                SqlCommand cmd = new SqlCommand(StoredProcedures.DeleteOrder, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(StoredProcedureParameters.orderId, orderId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
            }
            return true;
        }

        public async Task<Orders> GetOrderById(int orderId)
        {
            Orders ord = new Orders();
            using(SqlConnection con=_connectionFactory.Midland1SqlConnectionString())
            {
                SqlCommand cmd = new SqlCommand(StoredProcedures.GetOrderById, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(StoredProcedureParameters.orderId, orderId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, ClassNames.Orders);
                foreach(DataRow row in ds.Tables[ClassNames.Orders].Rows)
                {
                    ord.orderId = Convert.ToInt16(row[StoredProcedureParameters.orderId]);
                    ord.orderName = Convert.ToString(row[StoredProcedureParameters.orderName]);
                    ord.orderLocation = Convert.ToString(row[StoredProcedureParameters.orderLocation]);
                }
            }
            return ord;
           
        }

        public async Task<List<Orders>> GetOrders()
        {
           using(SqlConnection con=_connectionFactory.Midland1SqlConnectionString())
            {
                List<Orders> lstord = new List<Orders>();
                SqlCommand cmd = new SqlCommand(StoredProcedures.GetOrder, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, ClassNames.Orders);
                foreach(DataRow row in ds.Tables[ClassNames.Orders].Rows)
                {
                    Orders ord = new Orders();
                    ord.orderId = Convert.ToInt16(row[StoredProcedureParameters.orderId]);
                    ord.orderName = Convert.ToString(row[StoredProcedureParameters.orderName]);
                    ord.orderLocation = Convert.ToString(row[StoredProcedureParameters.orderLocation]);
                    lstord.Add(ord);
                }
                return lstord;
            }
        }

        public async Task<bool> UpdateOrder(Orders orderdetail)
        {
            using(SqlConnection con=_connectionFactory.Midland1SqlConnectionString())
            {
                SqlCommand cmd=new SqlCommand(StoredProcedures.UpdateOrder, con);
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(StoredProcedureParameters.orderId, orderdetail.orderId);
                cmd.Parameters.AddWithValue(StoredProcedureParameters.orderName, orderdetail.orderName);
                cmd.Parameters.AddWithValue(StoredProcedureParameters.orderLocation, orderdetail.orderLocation);
                SqlDataAdapter da=new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds,ClassNames.Orders);
                return true;
            }
        }
    }
}
