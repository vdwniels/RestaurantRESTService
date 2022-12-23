using RestaurantBL.Interfaces;
using RestaurantBL.Model;
using RestaurantDL.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDL.Repositories
{
    public class UserRepositoryADO : IUserRepository
    {
        private string connectionstring;

        public UserRepositoryADO(string connectionstring)
        {
            this.connectionstring = connectionstring;
        }

        public User AddUser(User user)
        {
            string sql = @"INSERT INTO Users(UserName,UserEmail,UserPhoneNumber,UserPostalCode,UserTown,UserStreet,UserNumber) output Inserted.CustomerId Values (@UserName,@UserEmail,@UserPhoneNumber,@UserPostalCode,@UserTown,@UserStreet,@UserNumber)";
            SqlConnection conn = new SqlConnection(connectionstring);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@UserName", user.Name);
                    cmd.Parameters.AddWithValue("@UserEmail", user.Email);
                    cmd.Parameters.AddWithValue("@UserPhoneNumber", user.PhoneNumber);
                    cmd.Parameters.AddWithValue("@UserPostalCode", user.Location.PostalCode);
                    cmd.Parameters.AddWithValue("@UserTown", user.Location.Town);

                    if (user.Location.Street == null) cmd.Parameters.AddWithValue("@UserStreet", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@UserStreet", user.Location.Street);

                    if (user.Location.Number == null) cmd.Parameters.AddWithValue("@UserNumber", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@UserNumber", user.Location.Number);

                    int newId = (int)cmd.ExecuteScalar();
                    user.SetCustomerNumber(newId);
                    return user;
                }
                catch (Exception ex)
                {
                    throw new UserRepositoryADOException("UserRepositoryADO - AddUser", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public User GetUser(int customerNumber)
        {
            string query = "SELECT * FROM Users where CustomerId=@CustomerId";
            SqlConnection conn = new SqlConnection(connectionstring);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@CustomerId", customerNumber);
                    IDataReader dataReader = cmd.ExecuteReader();
                    dataReader.Read();
                    Location l = new Location((int)dataReader["UserPostalCode"], (string)dataReader["UserTown"]);

                    if(dataReader["UserStreet"] != DBNull.Value) l.SetStreet((string)dataReader["UserStreet"]);
                    if (dataReader["UserNumber"] != DBNull.Value) l.SetNumber((string)dataReader["UserNumber"]);

                    User u = new User(customerNumber, (string)dataReader["UserName"], (string)dataReader["UserEmail"], (string)dataReader["UserPhoneNumber"],l  );
                    dataReader.Close();
                    return u;
                }
                catch (Exception ex)
                {
                    throw new UserRepositoryADOException("GetUser", ex);
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        public void UnsubscribeUser(int customerNumber)
        {
            string query = @"UPDATE Users SET UserIsDeleted = 1 WHERE CustomerId=@CustomerId";
            SqlConnection conn = new SqlConnection(connectionstring);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@CustomerId", customerNumber);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new UserRepositoryADOException("UserRepositoryADO - UnsubscribeUser", ex);
                }
                finally
                {
                    conn.Close();
                }

            }
        }

        public void UpdateUser(User user)
        {
            string query = @"UPDATE Users SET UserName=@UserName, UserEmail=@UserEmail, UserPhoneNumber=@UserPhoneNumber, UserPostalCode=@UserPostalCode, UserTown = @UserTown, UserStreet=@UserStreet, UserNumber=@UserNumber WHERE CustomerId=@CustomerId";
            SqlConnection conn = new SqlConnection(connectionstring);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@CustomerId", user.CustomerNumber);
                    cmd.Parameters.AddWithValue("@UserName", user.Name);
                    cmd.Parameters.AddWithValue("@UserEmail", user.Email);
                    cmd.Parameters.AddWithValue("@UserPhoneNumber", user.PhoneNumber);
                    cmd.Parameters.AddWithValue("@UserPostalCode", user.Location.PostalCode);
                    cmd.Parameters.AddWithValue("@UserTown", user.Location.Town);
                    if (user.Location.Street == null) cmd.Parameters.AddWithValue("@UserStreet", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@UserStreet", user.Location.Street);
                    if (user.Location.Number == null) cmd.Parameters.AddWithValue("@UserNumber", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@UserNumber", user.Location.Number);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new UserRepositoryADOException("UserRepositoryADO - UnsubscribeUser", ex);
                }
                finally
                {
                    conn.Close();
                }

            }

        }

        public bool UserExists(string phoneNumber, string email)
        {
            string query = @"Select count(*) from Users where UserPhoneNumber=@UserPhoneNumber AND UserEmail=@UserEmail AND UserIsDeleted = 0";
            SqlConnection conn = new SqlConnection(connectionstring);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@UserEmail", email);
                    cmd.Parameters.AddWithValue("@UserPhoneNumber", phoneNumber);
                    int n = (int)cmd.ExecuteScalar();
                    if (n > 0) return true;
                    else return false;
                }
                catch(Exception ex)
                {
                    throw new UserRepositoryADOException("UserRepositoryADO - UserExists", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public bool UserExists(int customerNumber)
        {
            string query = @"Select count(*) from Users where CustomerId=@CustomerId AND UserIsDeleted = 0";
            SqlConnection conn = new SqlConnection(connectionstring);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@CustomerId", customerNumber);
                    int n = (int)cmd.ExecuteScalar();
                    if (n > 0) return true;
                    else return false;
                }
                catch (Exception ex)
                {
                    throw new UserRepositoryADOException("UserRepositoryADO - UserExists", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
