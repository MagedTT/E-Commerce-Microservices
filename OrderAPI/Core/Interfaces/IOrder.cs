using System.Linq.Expressions;
using CoreLib.Interfaces;
using OrderAPI.Core.Models;

namespace OrderAPI.Core.Interfaces;

public interface IOrder : ICrud<Order>
{
    Task<IEnumerable<Order>> GetOrdersAsync(Expression<Func<Order, bool>> predicate);
}