using System.Linq.Expressions;
using CoreLib.Interfaces;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Core.Interfaces;
using OrderAPI.Core.Models;
using OrderAPI.Infrastructure.Data;

namespace OrderAPI.Infrastructure.Repos;

public class OrderRepo : IOrder
{
    private readonly OrderDbContext _context;
    public OrderRepo(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseMessage> CreateAsync(Order entity)
    {
        try
        {
            var order = _context.Set<Order>().Add(entity).Entity;
            await _context.SaveChangesAsync();

            if (order.Id > 0)
                return new ResponseMessage(true, "Order Placed successfully!");

            return new ResponseMessage(false, "An Error occured!");
        }
        catch (Exception)
        {
            return new ResponseMessage(false, "An Error occured!");
        }
    }


    public async Task<ResponseMessage> DeleteAsync(Order entity)
    {
        try
        {
            var order = GetByIdAsync(entity.Id);

            if (order is null)
                return new ResponseMessage(false, "Can't Find the Order to be Delete!");

            _context.Set<Order>().Remove(entity);

            await _context.SaveChangesAsync();

            return new ResponseMessage(true, "Order Deleted Successfully!");
        }
        catch (Exception)
        {
            return new ResponseMessage(false, "An Error occured!");
        }
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        try
        {
            var order = await _context.Set<Order>().FindAsync(id);

            if (order is null)
                return null!;

            return order;
        }
        catch (Exception)
        {
            throw new Exception("An Error occured!");
        }
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        try
        {
            var orders = await _context.Set<Order>().AsNoTracking().ToListAsync();

            if (orders is null)
                return null!;

            return orders;
        }
        catch (Exception)
        {
            throw new Exception("An Error occured!");
        }
    }

    public async Task<Order> GetByAsync(Expression<Func<Order, bool>> predicate)
    {
        try
        {
            var order = await _context.Set<Order>().Where(predicate).FirstOrDefaultAsync();

            if (order is null)
                return null!;

            return order;
        }
        catch (Exception)
        {
            throw new Exception("An Error occured!");
        }
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync(Expression<Func<Order, bool>> predicate)
    {
        try
        {
            var orders = await _context.Set<Order>().Where(predicate).ToListAsync();

            if (orders is null)
                return null!;
            return orders;
        }
        catch (Exception)
        {
            throw new Exception("An Error occured!");
        }
    }

    public async Task<ResponseMessage> UpdateAsync(Order entity)
    {
        try
        {
            var order = await GetByIdAsync(entity.Id);

            if (order is null)
                return new ResponseMessage(false, "Can't Find Order!");

            _context.Entry(order).State = EntityState.Detached;

            _context.Set<Order>().Update(entity);

            await _context.SaveChangesAsync();

            return new ResponseMessage(true, "Order Updated Successfully!");
        }
        catch (Exception)
        {
            return new ResponseMessage(false, "An Error occured!");
        }
    }
}