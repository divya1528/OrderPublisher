using ApplicationCore.Model;
using Infrastructure.Data;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        IContext _context;
        public UserRepository(IContext context)
        {
            _context = context;
        }
        public async Task<User> Get(LoginModel loginCreds)
        {
            return await _context
                           .Users
                           .Find(p => p.Email == loginCreds.email && p.Password == loginCreds.password)
                           .FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetAll()
        {
            return await _context
                           .Users
                           .Find(p => true).ToListAsync(); ;
        }
    }
}
