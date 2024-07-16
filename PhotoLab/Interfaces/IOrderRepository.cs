using PhotoLab.Models;

namespace PhotoLab.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAll();
        Task<IEnumerable<Service>> GetServicesList();
        Task<Service> GetSelectedService(int id);
        Task<Order> GetById(int id);
        Task<IEnumerable<Order>> GetByService(string service);
        Task<bool> Add(Order order);
        Task<bool> Update(Order order);
        Task<bool> Delete(Order order);
        Task<bool> Save();
    }
}
