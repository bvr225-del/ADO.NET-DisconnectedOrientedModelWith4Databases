using ADO.NET_DisconnectedOrientedModelWith4Databases.Dtos;
using ADO.NET_DisconnectedOrientedModelWith4Databases.Interfaces;
using ADO.NET_DisconnectedOrientedModelWith4Databases.Models;

namespace ADO.NET_DisconnectedOrientedModelWith4Databases.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository=orderRepository;
        }
        public async Task<int> AddOrder(OrdersDto orderdetail)
        {//Here i am converting employeedto object data into employee clas object
            Orders ord=new Orders();
            ord.orderId = orderdetail.orderId;
            ord.orderName = orderdetail.orderName;
            ord.orderLocation = orderdetail.orderLocation;
            var res=await _orderRepository.AddOrder(ord);
            return res;

        }

        public async Task<bool> DeleteOrder(int orderId)
        {
          await _orderRepository.DeleteOrder(orderId);
            return true;
        }

        public async Task<OrdersDto> GetOrderById(int orderId)
        {
           var res=await _orderRepository.GetOrderById(orderId);
            OrdersDto ordDto=new OrdersDto();
            ordDto.orderId=res.orderId;
            ordDto.orderName=res.orderName;
            ordDto.orderLocation=res.orderLocation;
            return ordDto;
        }

        public async Task<List<OrdersDto>> GetOrders()
        {
            List<OrdersDto> lstordDto=new List<OrdersDto>();
            var res=await _orderRepository.GetOrders();
            foreach(Orders ord in res)
            {
                OrdersDto ordDto=new OrdersDto();
                ordDto.orderId = ord.orderId;
                ordDto.orderName = ord.orderName;
                ordDto.orderLocation = ord.orderLocation;
                lstordDto.Add(ordDto);

            }
            return lstordDto;
           
        }

        public async Task<bool> UpdateOrder(OrdersDto orderdetail)
        {
            //here we are transfer the data from employeedto object to employee object and pass to repository layer.
            Orders ord=new Orders();
            ord.orderId=orderdetail.orderId;
            ord.orderName=orderdetail.orderName;
            ord.orderLocation=orderdetail.orderLocation;
            await _orderRepository.UpdateOrder(ord);
            return true;

        }
    }
}
