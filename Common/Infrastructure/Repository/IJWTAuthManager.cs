using ApplicationCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repository
{
    public interface IJWTAuthManager
    {
        Response<string> GenerateJWT(User user);
       // Response<T> Execute_Command<T>(string query, DynamicParameters sp_params);
        Response<List<T>> getUserList<T>();
    }
}
