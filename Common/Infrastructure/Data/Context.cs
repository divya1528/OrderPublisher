using ApplicationCore.Abstract;
using ApplicationCore.Entities;
using ApplicationCore.Model;
using MongoDB.Driver;

namespace Infrastructure.Data
{
    public class Context : IContext
    {

        public Context(IConfigSettings settings)
        {
            var client = new MongoClient(settings.OrderDBConnString);
            var database = client.GetDatabase(settings.OrderDBName);
            Orders = database.GetCollection<Order>(settings.OrderDBCollection);
            Users = database.GetCollection<User>("User");
            //OrderContextSeed.SeedData(Orders);
        }

        public IMongoCollection<Order> Orders { get; }

        public IMongoCollection<User> Users { get; }

    }
}
