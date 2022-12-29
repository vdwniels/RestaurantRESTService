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
            throw new NotImplementedException();
        }

        public Reservation GetReservation(int reservationNumber)
        {
            throw new NotImplementedException();
        }

        public List<Reservation> GetReservations(int restaurantId, DateTime? startDate, DateTime? endDate)
        {
            throw new NotImplementedException();
        }

        public bool reservationExists(int reservationId)
        {
            throw new NotImplementedException();
        }

        public List<Reservation> SearchReservations(int customerId, DateTime? startDate, DateTime? endDate)
        {
            throw new NotImplementedException();
        }


        public void UpdateReservation(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public List<Table> SelectFreeTables(int restaurantId, DateTime reservationtime)
        {
            string query = "Select * from tables where TableId not in (SELECT t.TableId from tables t Join Reservations r ON t.RestaurantId = r.RestaurantId AND t.TableNumber = r.TableNumberResevation where r.DateAndTime>@startTime and r.DateAndTime<@endTime and r.RestaurantId = @restaurantId and t.TableIsDeleted = 0 and r.ReservationIsDeleted = 0) and RestaurantId = @restaurantId;";
            SqlConnection conn = new SqlConnection(connectionstring);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;
                    DateTime start = reservationtime.AddMinutes(-60);
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

    }
}
