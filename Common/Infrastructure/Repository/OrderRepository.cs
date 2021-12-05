using ApplicationCore.Entities;
using Infrastructure.Data;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IContext _context;
        public OrderRepository(IContext context)
        {
            _context = context;
        }
        public async Task Create(Order order)
        {
            await _context.Orders.InsertOneAsync(order);
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _context
                            .Orders
                            .Find(p => true)
                            .ToListAsync();
        }

        public async Task<bool> Update(string orderid, string status)
        {
            var order = await _context.Orders.Find(p => p.Id == orderid).FirstOrDefaultAsync();
            if (order != null)
            {
                order.Status = status;
                var updateResult = await _context.Orders.UpdateOneAsync(Builders<Order>.Filter.Eq("Id", orderid), Builders<Order>.Update.Set(rec => rec.Status, status));
                
                return updateResult.IsAcknowledged
                                && updateResult.ModifiedCount > 0;
            }
            return false;

        }
    }
}
