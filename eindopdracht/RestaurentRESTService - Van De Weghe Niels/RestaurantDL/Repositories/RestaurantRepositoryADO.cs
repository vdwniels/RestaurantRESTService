using RestaurantBL.Interfaces;
using RestaurantBL.Model;
using RestaurantDL.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDL.Repositories
{
    public class RestaurantRepositoryADO : IRestautantRepository
    {
        private string connectionstring;

        public RestaurantRepositoryADO(string connectionstring)
        {
            this.connectionstring = connectionstring;
        }

        public Restaurant AddRestaurant(Restaurant restaurant)
        {
            string sql = @"INSERT INTO Restaurants(RestaurantName,Cuisine,RestaurantEmail,RestaurantPhoneNumber,RestaurantPostalCode,RestaurantTown,RestaurantStreet,RestaurantNumber) output Inserted.RestaurantId Values (@RestaurantName,@Cuisine,@RestaurantEmail,@RestaurantPhoneNumber,@RestaurantPostalCode,@RestaurantTown,@RestaurantStreet,@RestaurantNumber)";
            SqlConnection conn = new SqlConnection(connectionstring);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@RestaurantName", restaurant.Name);
                    cmd.Parameters.AddWithValue("@Cuisine", restaurant.Cuisine);
                    cmd.Parameters.AddWithValue("@RestaurantEmail", restaurant.Email);
                    cmd.Parameters.AddWithValue("@RestaurantPhoneNumber", restaurant.PhoneNumber);
                    cmd.Parameters.AddWithValue("@RestaurantPostalCode", restaurant.Location.PostalCode);
                    cmd.Parameters.AddWithValue("@RestaurantTown", restaurant.Location.Town);

                    if (restaurant.Location.Street == null) cmd.Parameters.AddWithValue("@RestaurantStreet", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@RestaurantStreet", restaurant.Location.Street);

                    if (restaurant.Location.Number == null) cmd.Parameters.AddWithValue("@RestaurantNumber", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@RestaurantNumber", restaurant.Location.Number);

                    int newId = (int)cmd.ExecuteScalar();
                    restaurant.SetRestaurantId(newId);
                    return restaurant;
                }
                catch (Exception ex)
                {
                    throw new UserRepositoryADOException("RestaurantRepositoryADO - AddRestaurant", ex);
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        public void DeleteRestaurant(int restaurantId)
        {
            string query = @"UPDATE Restaurants SET RestaurantIsDeleted = 1 WHERE RestaurantId=@RestaurantId";
            SqlConnection conn = new SqlConnection(connectionstring);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@RestaurantId", restaurantId);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new UserRepositoryADOException("RestaurantRepositoryADO - DeleteRestaurant", ex);
                }
                finally
                {
                    conn.Close();
                }

            }

        }

        public Restaurant GetRestaurant(int restaurantId)
        {
            string query = "SELECT * FROM Restaurants r  left join Tables t on t.RestaurantId=r.RestaurantId where r.RestaurantId=@RestaurantId And (t.TableIsDeleted=0 OR t.TableIsDeleted is null);";
            SqlConnection conn = new SqlConnection(connectionstring);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@RestaurantId", restaurantId);
                    IDataReader dataReader = cmd.ExecuteReader();
                    int loopnumber = 0;
                    Restaurant r = null;
                    while (dataReader.Read())
                    {
                        if (loopnumber == 0)
                        {
                            Location l = new Location((int)dataReader["RestaurantPostalCode"], (string)dataReader["RestaurantTown"]);

                            if (dataReader["RestaurantStreet"] != DBNull.Value) l.SetStreet((string)dataReader["RestaurantStreet"]);
                            if (dataReader["RestaurantNumber"] != DBNull.Value) l.SetNumber((string)dataReader["RestaurantNumber"]);
                            r = new Restaurant(restaurantId, (string)dataReader["RestaurantName"], l, (string)dataReader["Cuisine"], (string)dataReader["RestaurantEmail"], (string)dataReader["RestaurantPhoneNumber"]);
                        }
                        if (dataReader["TableId"] != DBNull.Value)
                        {
                            Table t = new Table((int)dataReader["TableId"],(int)dataReader["TableNumber"], (int)dataReader["Seats"], restaurantId);
                            r.AddTable(t);
                        }
                        loopnumber++;
                    }


                    dataReader.Close();
                    return r;
                }
                catch (Exception ex)
                {
                    throw new UserRepositoryADOException("GetRestaurant", ex);
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        public IReadOnlyList<Restaurant> GetRestaurantsWithFreeTables(DateTime date, int seats)
        {
            throw new NotImplementedException();
        }

        public bool HasTables(int restaurantId)
        {
            string query = @"Select Count(*) from Tables  where RestaurantId=@RestaurantId AND TableIsDeleted = 0  ";
            SqlConnection conn = new SqlConnection(connectionstring);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@RestaurantId", restaurantId);
                    int n = (int)cmd.ExecuteScalar();
                    if (n > 0) return true;
                    else return false;
                }
                catch (Exception ex)
                {
                    throw new RestaurantRepositoryADOException("RestaurantRepositoryADO - HasTables", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public bool RestaurantExists(string phoneNumber, string email)
        {
            string query = @"Select count(*) from Restaurants where RestaurantPhoneNumber=@RestaurantPhoneNumber AND RestaurantEmail=@RestaurantEmail AND RestaurantIsDeleted = 0";
            SqlConnection conn = new SqlConnection(connectionstring);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@RestaurantEmail", email);
                    cmd.Parameters.AddWithValue("@RestaurantPhoneNumber", phoneNumber);
                    int n = (int)cmd.ExecuteScalar();
                    if (n > 0) return true;
                    else return false;
                }
                catch (Exception ex)
                {
                    throw new RestaurantRepositoryADOException("RestaurantRepositoryADO - RestaurantExists", ex);
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        public bool RestaurantExists(int restaurantId)
        {
            string query = @"Select count(*) from Restaurants where RestaurantId=@RestaurantId AND RestaurantIsDeleted = 0";
            SqlConnection conn = new SqlConnection(connectionstring);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@RestaurantId", restaurantId);
                    int n = (int)cmd.ExecuteScalar();
                    if (n > 0) return true;
                    else return false;
                }
                catch (Exception ex)
                {
                    throw new RestaurantRepositoryADOException("RestaurantRepositoryADO - RestaurantExists", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public IReadOnlyList<Restaurant> SearchRestaurantOnLocationAndOrCuisine(int? postalcode, string? cuisine)
        {
            throw new NotImplementedException();
        }

        public void UpdateRestaurant(Restaurant restaurant)
        {
            string query = @"UPDATE Restaurants SET RestaurantName=@RestaurantName,Cuisine=@Cuisine, RestaurantEmail=@RestaurantEmail, RestaurantPhoneNumber=@RestaurantPhoneNumber, RestaurantPostalCode=@RestaurantPostalCode, RestaurantTown = @RestaurantTown, RestaurantStreet=@RestaurantStreet, RestaurantNumber=@RestaurantNumber WHERE RestaurantId=@RestaurantId";
            SqlConnection conn = new SqlConnection(connectionstring);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@RestaurantId", restaurant.RestaurantId);
                    cmd.Parameters.AddWithValue("@Cuisine", restaurant.Cuisine);
                    cmd.Parameters.AddWithValue("@RestaurantName", restaurant.Name);
                    cmd.Parameters.AddWithValue("@RestaurantEmail", restaurant.Email);
                    cmd.Parameters.AddWithValue("@RestaurantPhoneNumber", restaurant.PhoneNumber);
                    cmd.Parameters.AddWithValue("@RestaurantPostalCode", restaurant.Location.PostalCode);
                    cmd.Parameters.AddWithValue("@RestaurantTown", restaurant.Location.Town);
                    if (restaurant.Location.Street == null) cmd.Parameters.AddWithValue("@RestaurantStreet", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@RestaurantStreet", restaurant.Location.Street);
                    if (restaurant.Location.Number == null) cmd.Parameters.AddWithValue("@RestaurantNumber", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@RestaurantNumber", restaurant.Location.Number);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new UserRepositoryADOException("RestaurantRepositoryADO - UpdateRestaurant", ex);
                }
                finally
                {
                    conn.Close();
                }


            }
        }
    }
}
