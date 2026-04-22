using ADO.NET_DisconnectedOrientedModelWith4Databases.Dtos;

namespace ADO.NET_DisconnectedOrientedModelWith4Databases.Interfaces
{
    public interface IOrderService
    {
        Task <List<OrdersDto>> GetOrders();
        Task <OrdersDto>GetOrderById(int orderId);
        Task <int>AddOrder(OrdersDto orderdetail);
        Task<bool> UpdateOrder(OrdersDto orderdetail);
        Task<bool> DeleteOrder(int orderId);


    }
}
