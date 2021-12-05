using ApplicationCore.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
   public interface IUserRepository
    {
        Task<User> Get(LoginModel loginCreds);
        Task<List<User>> GetAll();
    }
}
