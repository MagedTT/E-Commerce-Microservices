using System.Net.Http.Json;
using OrderAPI.Core.Dtos;
using OrderAPI.Core.Interfaces;
using OrderAPI.Core.Mappers;

namespace OrderAPI.Core.Services;

public class OrderService : IOrderService
{
    private readonly IOrder _orders;
    private readonly HttpClient _httpClient;
    public OrderService(HttpClient httpClient, IOrder orderInterface)
    {
        _httpClient = httpClient;
        _orders = orderInterface;
    }

    public async Task<ProductDto> GetProductAsync(int productId)
    {
        // Call Product Api
        // Redirect this call to the api gateway
        // product api is not respondant to outsiders
        var product = await _httpClient.GetAsync($"/api/products/{productId}");

        if (!product.IsSuccessStatusCode)
            return null!;

        return (await product.Content.ReadFromJsonAsync<ProductDto>())!;
    }

    public async Task<UserDto> GetUserAsync(int userId)
    {
        // Call Product Api
        // Redirect this call to the api gateway
        // product api is not respondant to outsiders
        var getUser = await _httpClient.GetAsync($"/api/Authentication/{userId}"); // 

        if (!getUser.IsSuccessStatusCode)
            return null!;

        return (await getUser.Content.ReadFromJsonAsync<UserDto>())!;
    }

    public async Task<IEnumerable<OrderDto>> GetOrdersByClientIdAsync(int clientId)
    {
        var orders = await _orders.GetOrdersAsync(x => x.ClientId == clientId);

        if (!orders.Any())
            return null!;

        var ordersDto = orderMapper.FromEntity(orders);
        return ordersDto;
    }

    public async Task<OrderDetailsDto> GetOrderDetailsAsync(int orderId)
    {
        var order = await _orders.GetByIdAsync(orderId);

        if (order is null || order!.Id <= 0)
            return null!;

        return new OrderDetailsDto
        {
            OrderId = order.Id,
            ProductId = order.ProductId,
            Clientid = order.ClientId,

        };
    }
}