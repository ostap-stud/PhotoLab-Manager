using Microsoft.EntityFrameworkCore;
using PhotoLab.Data;
using PhotoLab.Interfaces;
using PhotoLab.Models;

namespace PhotoLab.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly PhotoLabContext _context;

        public OrderRepository(PhotoLabContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(Order order)
        {
            _context.Add(order);
            return await Save();
        }

        public async Task<bool> Delete(Order order)
        {
            _context.Remove(order);
            return await Save();
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _context.Orders.Include(o => o.Service).ToListAsync();
        }

        public async Task<IEnumerable<Service>> GetServicesList()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<Service> GetSelectedService(int id)
        {
            return await (_context.Services.FirstOrDefaultAsync(s => s.ServiceId == id));
        }

        public async Task<Order> GetById(int id)
        {
            return await _context.Orders.Include(o => o.Service).FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task<IEnumerable<Order>> GetByService(string serviceName)
        {
            return await (_context.Orders.Where(o => o.Service.ServiceName.Equals(serviceName)).ToListAsync());
        }

        public async Task<bool> Save()
        {
            int saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> Update(Order order)
        {
            _context.Update(order);
            return await Save();
        }
    }
}
