using System.Linq.Expressions;
using CoreLib.Interfaces;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Application.Interfaces;
using ProductAPI.Core.Models;
using ProductAPI.Infrastructure.Data;

namespace ProductApi.Infrastructure.Repos;

public class ProductRepo : IProduct
{
    private readonly ProductDbContext _context;
    public ProductRepo(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseMessage> CreateAsync(Product dbProduct)
    {
        try
        {
            bool productExists = await _context.Set<Product>().AnyAsync(x => x.Name == dbProduct.Name);

            if (productExists)
                return new ResponseMessage(false, $"{dbProduct.Name} Already Exists");

            _context.Set<Product>().Add(dbProduct);
            await _context.SaveChangesAsync();

            return new ResponseMessage(true, $"{dbProduct.Name} Added Successfully");
        }
        catch (Exception)
        {
            return new ResponseMessage(false, "An Error occured!");
        }
    }

    public async Task<ResponseMessage> DeleteAsync(Product dbProduct)
    {
        try
        {
            var product = await GetByIdAsync(dbProduct.Id);

            if (product is null)
                return new ResponseMessage(false, $"Can't Find Product: `{dbProduct.Name}`");

            _context.Set<Product>().Remove(product);

            await _context.SaveChangesAsync();

            return new ResponseMessage(true, $"Deleted `{dbProduct.Name}` successfully!");
        }
        catch (Exception)
        {
            return new ResponseMessage(false, "An Error Occured!");
        }
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        try
        {
            var product = await _context.Set<Product>().FindAsync(id);

            if (product is null)
                return null!;

            return product;
        }
        catch (Exception)
        {
            throw new Exception("An Error occured!");
        }
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        try
        {
            var products = await _context.Set<Product>().AsNoTracking().ToListAsync();

            if (products is null)
                return null!;
            return products;
        }
        catch (Exception)
        {
            throw new InvalidOperationException("An Error occured!");
        }
    }

    public async Task<Product> GetByAsync(Expression<Func<Product, bool>> predicate)
    {
        try
        {
            var product = await _context.Set<Product>().Where(predicate).FirstOrDefaultAsync()!;

            if (product is null)
                return null!;

            return product;
        }
        catch (Exception)
        {
            throw new InvalidOperationException("An Error occured!");
        }
    }

    public async Task<ResponseMessage> UpdateAsync(Product dbProduct)
    {
        try
        {
            var product = await GetByIdAsync(dbProduct.Id);

            if (product is null)
                return new ResponseMessage(false, $"Can't Find Product: `{dbProduct.Name}`");

            _context.Entry(product).State = EntityState.Detached;
            _context.Products.Update(dbProduct);
            await _context.SaveChangesAsync();

            return new ResponseMessage(true, $"Product: `{dbProduct.Name}` Updated Successfully!");
        }
        catch (Exception)
        {
            return new ResponseMessage(false, "An Error occured!");
        }
    }
}