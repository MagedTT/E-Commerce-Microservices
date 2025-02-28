using OrderAPI.Core.Dtos;
using OrderAPI.Core.Models;

namespace OrderAPI.Core.Mappers;

public static class orderMapper
{
    public static Order ToEntity(OrderDto order)
    {
        return new Order
        {
            Id = order.Id,
            ProductId = order.ProductId,
            ClientId = order.ClientId,
            OrderDate = order.OrderDate,
            Quantity = order.Quantity
        };
    }

    public static OrderDto FromEntity(Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            ProductId = order.ProductId,
            ClientId = order.ClientId,
            Quantity = order.Quantity,
            OrderDate = order.OrderDate
        };
    }

    public static IEnumerable<OrderDto> FromEntity(IEnumerable<Order> orders)
    {
        return orders.Select(x => new OrderDto() { Id = x.Id, ClientId = x.ClientId, ProductId = x.ProductId, Quantity = x.Quantity, OrderDate = x.OrderDate });
    }
}