using CoreLib.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderAPI.Core.Dtos;
using OrderAPI.Core.Interfaces;
using OrderAPI.Core.Mappers;
using OrderAPI.Core.Services;

namespace OrderApi.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly IOrder _orders;
    private readonly IOrderService _orderServices;

    public OrdersController(IOrder orders, IOrderService orderServices)
    {
        _orders = orders;
        _orderServices = orderServices;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
    {
        var orders = await _orders.GetAllAsync();
        if (!orders.Any())
        {
            return NotFound("No Orders Found");
        }

        var ordersDTO = orderMapper.FromEntity(orders);

        if (!ordersDTO.Any())
            return NotFound("No Orders Found");

        return Ok(ordersDTO);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<OrderDto>> GetOrder(int id)
    {
        var order = await _orders.GetByIdAsync(id);

        if (order is null)
            return NotFound("Order Not Found");

        var orderDTO = orderMapper.FromEntity(order);
        return Ok(orderDTO);
    }

    [HttpGet("client/{clientId}")]
    public async Task<ActionResult<OrderDto>> GetClientOrders(int clientId)
    {
        if (clientId <= 0)
            return BadRequest("Invalid Client Id");

        var orders = await _orderServices.GetOrdersByClientIdAsync(clientId);

        if (!orders.Any())
            return NotFound("No Orders Found");

        return Ok(orders);
    }

    [HttpGet("details/{orderId}")]
    public async Task<ActionResult<OrderDetailsDto>> GetOrderDetails(int orderId)
    {
        if (orderId <= 0)
            return BadRequest("Invalid Data");

        var orderDetails = await _orderServices.GetOrderDetailsAsync(orderId);

        if (orderDetails is null)
            return NotFound("No Order Found");

        return Ok(orderDetails);
    }

    [HttpPost]
    public async Task<ActionResult<ResponseMessage>> CreateOrder(OrderDto orderDto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Bad Request");

        var orderEntity = orderMapper.ToEntity(orderDto);

        var response = await _orders.CreateAsync(orderEntity);

        if (response.Status)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpPut]
    public async Task<ActionResult<ResponseMessage>> UpdateOrder(OrderDto orderDto)
    {
        var orderEntity = orderMapper.ToEntity(orderDto);

        var response = await _orders.UpdateAsync(orderEntity);

        if (response.Status is true)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpDelete]
    public async Task<ActionResult<ResponseMessage>> DeleteOrder(OrderDto orderDto)
    {
        var orderEntity = orderMapper.ToEntity(orderDto);

        var response = await _orders.DeleteAsync(orderEntity);

        if (response.Status is true)
            return Ok(response);

        return BadRequest(response);
    }
}