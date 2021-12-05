using ApplicationCore.Entities;
using ApplicationCore.Model;
using MongoDB.Driver;


namespace Infrastructure.Data
{
    public interface IContext
    {
        IMongoCollection<Order> Orders { get; }

        IMongoCollection<User> Users { get; }
    }
}
