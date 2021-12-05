
using ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IOrderRepository
    {
        Task Create(Order order);
        Task<IEnumerable<Order>> GetOrders();
        Task<bool> Update(string orderid, string status);
    }
}
