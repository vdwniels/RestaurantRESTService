using RestaurantBL.Interfaces;
using RestaurantBL.Model;
using RestaurantDL.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDL.Repositories
{
    public class TableRepositoryADO : ITableRepository
    {
        private string connectionstring;

        public TableRepositoryADO(string connectionstring)
        {
            this.connectionstring = connectionstring;
        }

        public Table AddTable(Table table)// TODO test
        {
            string sql = @"INSERT INTO Tables(RestaurantId,TableNumber,Seats) output Inserted.TableId Values (@RestaurantId,@TableNumber,@Seats)";
            SqlConnection conn = new SqlConnection(connectionstring);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@RestaurantId", table.RestaurantId);
                    cmd.Parameters.AddWithValue("@TableNumber", table.TableNumber);
                    cmd.Parameters.AddWithValue("@Seats", table.Seats);

                    int newId = (int)cmd.ExecuteScalar();
                    table.SetTableId(newId);
                    return table;
                }
                catch (Exception ex)
                {
                    throw new TableRepositoryADOException("TableRepositoryADO - AddTable", ex);
                }
                finally
                {
                    conn.Close();
                }

            }
        }

        public void DeleteTable(int tableId)
        {
            throw new NotImplementedException();
        }

        public List<Table> GetAllTablesOfRestaurant(int restaurantId)
        {
            throw new NotImplementedException();
        }

        public Table GetTable(int tableId)
        {
            throw new NotImplementedException();
        }

        public bool TableExists(int tableNumber, int restaurantId)
        {
            string query = @"Select count(*) from Tables where TableNumber=@TableNumber AND RestaurantId=@RestaurantId AND TableIsDeleted = 0";
            SqlConnection conn = new SqlConnection(connectionstring);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@TableNumber", tableNumber);
                    cmd.Parameters.AddWithValue("@RestaurantId", restaurantId);
                    int n = (int)cmd.ExecuteScalar();
                    if (n > 0) return true;
                    else return false;
                }
                catch (Exception ex)
                {
                    throw new TableRepositoryADOException("TableRepositoryADO - TableExists", ex);
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        public bool TableExists(int tableId)
        {
            throw new NotImplementedException();
        }

        public void UpdateTable(Table table)
        {
            throw new NotImplementedException();
        }
    }
}
