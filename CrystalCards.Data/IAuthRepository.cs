using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CrystalCards.Models;

namespace CrystalCards.Data
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}
