using RestaurantBL.Exceptions;
using RestaurantBL.Interfaces;
using RestaurantBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Services
{
    public class UserService
    {
        private IUserRepository repo;

        public UserService(IUserRepository repo)
        {
            this.repo = repo;
        }

        public User GetUser(int customerNumber)
        {
            try
            {
                if (!repo.UserExists(customerNumber)) throw new UserServiceException("UserService - Getuser - User does not exist");
                return repo.GetUser(customerNumber);
            }
            catch(UserServiceException)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw new UserServiceException("GetUser", ex);
            }
        }

        public User AddUser(User user)
        {
            try
            {
                if (user == null) throw new UserServiceException("UserService - Adduser - No user entry");
                if (repo.UserExists(user.PhoneNumber, user.Email)) throw new UserServiceException("UserService - AddUser - User with this phonenumber and/or email already exists");
                User userWithId = repo.AddUser(user);
                return userWithId;
            }
            catch (UserServiceException)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw new UserServiceException("AddUser", ex);
            }
        }

        public void UpdateUser(User user)
        {
            try
            {
                if (user == null) throw new UserServiceException("UserService - Updateuser - No user entry");
                if (!repo.UserExists(user.CustomerNumber)) throw new UserServiceException("UserService - Updateuser - User doesn't exist");
                User currentUserData = repo.GetUser(user.CustomerNumber);
                if (user == currentUserData) throw new UserServiceException("UserService - Updateuser - No different values");// operator overload
                repo.UpdateUser(user);
            }
            catch (UserServiceException)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw new UserServiceException("UpdateUser", ex);
            }
        }

        public void UnsubscribeUser(int customerNumber)
        {
            try
            {
                if (!repo.UserExists(customerNumber)) throw new UserServiceException("UserService - unsubscribeUser - User doesn't exist");
                repo.UnsubscribeUser(customerNumber);
            }
            catch (UserServiceException)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw new UserServiceException("UnsubscribeUser", ex);
            }
        }


    }
}
