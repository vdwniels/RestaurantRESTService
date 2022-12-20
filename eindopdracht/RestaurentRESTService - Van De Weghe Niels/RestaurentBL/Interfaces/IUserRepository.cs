using RestaurantBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User GetUser(int customerNumber);
        void UnsubscribeUser(int customerNumber);
        void UpdateUser(User user);
        bool UserExists(string phoneNumber, string email);
        bool UserExists(int customerNumber);
    }
}
