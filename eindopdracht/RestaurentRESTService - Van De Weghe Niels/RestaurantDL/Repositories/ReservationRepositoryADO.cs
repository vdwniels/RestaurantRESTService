using RestaurantBL.Interfaces;
using RestaurantBL.Model;
using RestaurantDL.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDL.Repositories
{
    public class ReservationRepositoryADO : IReservationRepository
    {
        private string connectionstring;

        public ReservationRepositoryADO(string connectionstring)
        {
            this.connectionstring = connectionstring;
        }

        public Reservation AddReservation(Reservation reservation)
        {
            string sql = @"INSERT INTO Reservations(RestaurantId,CustomerId,ReservationSeats,DateAndTime,TableNumberResevation) output Inserted.ReservationNumber Values (@RestaurantId,@CustomerId,@ReservationSeats,@DateAndTime,@TableNumberReservation)";
            SqlConnection conn = new SqlConnection(connectionstring);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@RestaurantId", reservation.RestaurantInfo.RestaurantId);
                    cmd.Parameters.AddWithValue("@CustomerId", reservation.Customer.CustomerNumber);
                    cmd.Parameters.AddWithValue("@ReservationSeats", reservation.Seats);
                    cmd.Parameters.AddWithValue("@DateAndTime", reservation.DateAndHour);
                    cmd.Parameters.AddWithValue("@TableNumberReservation", reservation.Tablenumber);
                    int newId = (int)cmd.ExecuteScalar();
                    reservation.SetReservationNumber(newId);
                    return reservation;
                }
                catch (Exception ex)
                {
                    throw new UserRepositoryADOException("ReservationRepositoryADO - AddReservation", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void CancelReservation(int reservationId)
        {
            string query = @"UPDATE Reservations SET ReservationIsDeleted = 1 WHERE ReservationNumber=@ReservationNumber AND DateAndTime > @currentTime";
            
            SqlConnection conn = new SqlConnection(connectionstring);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@ReservationNumber", reservationId);
                    cmd.Parameters.AddWithValue("@currentTime", DateTime.Now);
                    int rowsaffected = cmd.ExecuteNonQuery();
                    if (rowsaffected == 0) throw new ReservationRepositoryADOException("ReservarionRepositoryADO - CancelReservation - Unable to cancel reservations in the past");
                }
                catch (ReservationRepositoryADOException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new UserRepositoryADOException("ReservationRepositoryADO - CancelReservation", ex);
                }
                finally
                {
                    conn.Close();
                }

            }
        }

        public Reservation GetReservation(int reservationNumber)
        {
            string query = "select * from Reservations reserv JOIN Users u on reserv.CustomerId=u.CustomerId JOIN Restaurants rest on rest.RestaurantId=reserv.RestaurantId where reserv.ReservationNumber=@ReservationNumber and ReservationIsDeleted=0";
            SqlConnection conn = new SqlConnection(connectionstring);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@ReservationNumber", reservationNumber);
                    IDataReader dataReader = cmd.ExecuteReader();
                    dataReader.Read();
                    Location lu = new Location((int)dataReader["UserPostalCode"], (string)dataReader["UserTown"]);

                    if (dataReader["UserStreet"] != DBNull.Value) lu.SetStreet((string)dataReader["UserStreet"]);
                    if (dataReader["UserNumber"] != DBNull.Value) lu.SetNumber((string)dataReader["UserNumber"]);

                    User u = new User((int)dataReader["CustomerId"], (string)dataReader["UserName"], (string)dataReader["UserEmail"], (string)dataReader["UserPhoneNumber"], lu);

                    Location lr = new Location((int)dataReader["RestaurantPostalCode"], (string)dataReader["RestaurantTown"]);

                    if (dataReader["RestaurantStreet"] != DBNull.Value) lu.SetStreet((string)dataReader["RestaurantStreet"]);
                    if (dataReader["RestaurantNumber"] != DBNull.Value) lu.SetNumber((string)dataReader["RestaurantNumber"]);

                    Restaurant rest = new Restaurant((int)dataReader["RestaurantId"] ,(string)dataReader["RestaurantName"], lr, (string)dataReader["Cuisine"], (string)dataReader["RestaurantEmail"], (string)dataReader["RestaurantPhoneNumber"]);

                    Reservation reserv = new Reservation(reservationNumber, (int)dataReader["TableNumberResevation"], rest, u, (int)dataReader["ReservationSeats"], (DateTime)dataReader["DateAndTime"]);

                    dataReader.Close();
                    return reserv;
                }
                catch (Exception ex)
                {
                    throw new UserRepositoryADOException("GetReservation", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public List<Reservation> GetReservations(int restaurantId, DateTime? startDate, DateTime? endDate)
        {
            throw new NotImplementedException();
        }

        public bool reservationExists(int reservationId)
        {
            string query = @"Select count(*) from Reservations where ReservationNumber=@ReservationNumber AND ReservationIsDeleted = 0";
            SqlConnection conn = new SqlConnection(connectionstring);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@ReservationNumber", reservationId);
                    int n = (int)cmd.ExecuteScalar();
                    if (n > 0) return true;
                    else return false;
                }
                catch (Exception ex)
                {
                    throw new UserRepositoryADOException("reservationExists", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public List<Reservation> SearchReservations(int customerId, DateTime? startDate, DateTime? endDate)
        {
            SqlConnection conn = new SqlConnection(connectionstring);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();

                    #region queryconstruction
                    string query = "SELECT * FROM Reservations reserv join Restaurants rest on reserv.RestaurantId=rest.RestaurantId join Users u  on reserv.CustomerId=u.CustomerId where ";
                    if (startDate.HasValue)
                    {
                        query += "reserv.DateAndTime>@start AND ";
                        cmd.Parameters.AddWithValue("@start", startDate);
                    }
                    
                    if (endDate.HasValue)
                    {
                        query += "reserv.DateAndTime<@end AND ";
                        cmd.Parameters.AddWithValue("@end", endDate);

                    }
                    
                    query += "reserv.CustomerId=@customerId And reserv.ReservationIsDeleted=0;";
                    cmd.Parameters.AddWithValue("@customerId", customerId);
                    #endregion

                    cmd.CommandText = query;

                    IDataReader dataReader = cmd.ExecuteReader();
                    
                    List<Reservation> reservations = new List<Reservation>();
                    while (dataReader.Read())
                    {
                        Location lu = new Location((int)dataReader["UserPostalCode"], (string)dataReader["UserTown"]);

                        if (dataReader["UserStreet"] != DBNull.Value) lu.SetStreet((string)dataReader["UserStreet"]);
                        if (dataReader["UserNumber"] != DBNull.Value) lu.SetNumber((string)dataReader["UserNumber"]);

                        User u = new User((int)dataReader["CustomerId"], (string)dataReader["UserName"], (string)dataReader["UserEmail"], (string)dataReader["UserPhoneNumber"], lu);

                        Location lr = new Location((int)dataReader["RestaurantPostalCode"], (string)dataReader["RestaurantTown"]);

                        if (dataReader["RestaurantStreet"] != DBNull.Value) lu.SetStreet((string)dataReader["RestaurantStreet"]);
                        if (dataReader["RestaurantNumber"] != DBNull.Value) lu.SetNumber((string)dataReader["RestaurantNumber"]);

                        Restaurant rest = new Restaurant((int)dataReader["RestaurantId"], (string)dataReader["RestaurantName"], lr, (string)dataReader["Cuisine"], (string)dataReader["RestaurantEmail"], (string)dataReader["RestaurantPhoneNumber"]);

                        Reservation r = new Reservation((int)dataReader["ReservationNumber"], (int)dataReader["TableNumberResevation"], rest, u, (int)dataReader["ReservationSeats"], (DateTime)dataReader["DateAndTime"]);

                        reservations.Add(r);
                    }


                    dataReader.Close();
                    return reservations;
                }
                catch (Exception ex)
                {
                    throw new UserRepositoryADOException("SearchReservations", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }


        public void UpdateReservation(Reservation reservation)
        {
            string query = @"UPDATE Reservations SET DateAndTime=@DateAndTime, ReservationSeats=@Seats, TableNumberResevation=@TableNumberResevation WHERE ReservationNumber=@ReservationNumber";
            SqlConnection conn = new SqlConnection(connectionstring);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@DateAndTime", reservation.DateAndHour);
                    cmd.Parameters.AddWithValue("@Seats", reservation.Seats);
                    cmd.Parameters.AddWithValue("@TableNumberResevation", reservation.Tablenumber);
                    cmd.Parameters.AddWithValue("@ReservationNumber", reservation.ReservationNumber);
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

        public List<Table> SelectFreeTables(int restaurantId, DateTime reservationtime)
        {
            string query = "Select * from tables where TableId not in (SELECT t.TableId from tables t Join Reservations r ON t.RestaurantId = r.RestaurantId AND t.TableNumber = r.TableNumberResevation where r.DateAndTime>@startTime and r.DateAndTime<@endTime and r.RestaurantId = @restaurantId and t.TableIsDeleted = 0 and r.ReservationIsDeleted = 0) and RestaurantId = @restaurantId AND TableIsDeleted = 0;";
            SqlConnection conn = new SqlConnection(connectionstring);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;
                    DateTime start = reservationtime.AddMinutes(-90);
                    DateTime end = reservationtime.AddMinutes(90);
                    cmd.Parameters.AddWithValue("@startTime", start);
                    cmd.Parameters.AddWithValue("@endTime", end);
                    cmd.Parameters.AddWithValue("@restaurantId", restaurantId);

                    IDataReader dataReader = cmd.ExecuteReader();
                    Table t = null;
                    List<Table> tables = new List<Table>();
                    while (dataReader.Read())
                    {
                        t = new Table((int)dataReader["TableId"], (int)dataReader["TableNumber"], (int)dataReader["Seats"], (int)dataReader["RestaurantId"]);
                        tables.Add(t);
                    }


                    dataReader.Close();
                    return tables;
                }
                catch (Exception ex)
                {
                    throw new UserRepositoryADOException("SelectFreeTable", ex);
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        public List<Table> SelectFreeTables(int reservationNumber, int restaurantId, DateTime reservationtime)// same as overload but here the not-yet-updated reservaton's table is seen as free 
        {
            string query = "Select * from tables where TableId not in (SELECT t.TableId from tables t Join Reservations r ON t.RestaurantId = r.RestaurantId AND t.TableNumber = r.TableNumberResevation where r.DateAndTime>@startTime and r.DateAndTime<@endTime and r.RestaurantId = @restaurantId and t.TableIsDeleted = 0 and r.ReservationNumber!=@ReservationNumber  and r.ReservationIsDeleted = 0) and RestaurantId = @restaurantId AND TableIsDeleted = 0;";
            SqlConnection conn = new SqlConnection(connectionstring);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;
                    DateTime start = reservationtime.AddMinutes(-90);
                    DateTime end = reservationtime.AddMinutes(90);
                    cmd.Parameters.AddWithValue("@startTime", start);
                    cmd.Parameters.AddWithValue("@endTime", end);
                    cmd.Parameters.AddWithValue("@restaurantId", restaurantId);
                    cmd.Parameters.AddWithValue("@ReservationNumber", reservationNumber);

                    IDataReader dataReader = cmd.ExecuteReader();
                    Table t = null;
                    List<Table> tables = new List<Table>();
                    while (dataReader.Read())
                    {
                        t = new Table((int)dataReader["TableId"], (int)dataReader["TableNumber"], (int)dataReader["Seats"], (int)dataReader["RestaurantId"]);
                        tables.Add(t);
                    }


                    dataReader.Close();
                    return tables;
                }
                catch (Exception ex)
                {
                    throw new UserRepositoryADOException("SelectFreeTable", ex);
                }
                finally
                {
                    conn.Close();
                }
            }

        }

    }
}
