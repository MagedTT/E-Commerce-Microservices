using OrderAPI.Core.Dtos;

namespace OrderAPI.Core.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetOrdersByClientIdAsync(int clientId);
    Task<OrderDetailsDto> GetOrderDetailsAsync(int orderId);
}