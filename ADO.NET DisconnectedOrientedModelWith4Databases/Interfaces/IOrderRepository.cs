using ADO.NET_DisconnectedOrientedModelWith4Databases.Models;

namespace ADO.NET_DisconnectedOrientedModelWith4Databases.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Orders>> GetOrders();
        Task<Orders> GetOrderById(int orderId);
        Task<int> AddOrder(Orders orderdetail);
        Task<bool> DeleteOrder(int orderId);
        Task<bool> UpdateOrder(Orders orderdetail);
    }
}