using ADO.NET_DisconnectedOrientedModelWith4Databases.Dtos;
using ADO.NET_DisconnectedOrientedModelWith4Databases.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ADO.NET_DisconnectedOrientedModelWith4Databases.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService=orderService;
        }
        [HttpPost]
        [Route("AddOrder")]
        public async Task<IActionResult> Post([FromBody] OrdersDto ordDto)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "bad request");
                }
                else
                {
                    var ordData = await _orderService.AddOrder(ordDto);
                    return StatusCode(StatusCodes.Status201Created, ordData);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "sever not found");
            }
        }
        [HttpDelete]
        [Route("DeleteOrderById/{orderId}")]

        public async Task<IActionResult>Delete(int orderId)
        {
            if(orderId<0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "bad request");
            }
            try
            {
                var ordData = await _orderService.DeleteOrder(orderId);
                if(ordData==null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "ordData not found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, "deleted successfully");
                }
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }
        [HttpGet]
        [Route("GetOrders")]
        public async Task<IActionResult>GetOrders()
        {
            try
            {
                var ordData = await _orderService.GetOrders();
                    if(ordData==null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "bad request");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, ordData);
                }
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }
        [HttpGet]
        [Route("GetOrderById/{orderId}")]
        public async Task<IActionResult>GetOrderById(int orderId)
        {
            try
            {
                var ordData = await _orderService.GetOrderById(orderId);
                if(ordData==null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "bad request");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, ordData);
                }
            }
            catch( Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }
        [HttpPut]
        [Route("UpdateOrder")]
        public async Task<IActionResult> Put([FromBody]OrdersDto ordDto)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "bad request");
                }
                else
                {
                    var ordData = await _orderService.UpdateOrder(ordDto);
                    return StatusCode(StatusCodes.Status200OK, ordData);
                }
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }

        }
    }
}
