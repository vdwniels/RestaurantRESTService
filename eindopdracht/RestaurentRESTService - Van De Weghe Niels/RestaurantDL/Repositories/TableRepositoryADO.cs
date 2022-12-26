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
    public class TableRepositoryADO : ITableRepository
    {
        private string connectionstring;

        public TableRepositoryADO(string connectionstring)
        {
            this.connectionstring = connectionstring;
        }

        public Table AddTable(Table table)
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
            string query = @"UPDATE Tables SET tableIsDeleted = 1 WHERE TableId=@tableId";
            SqlConnection conn = new SqlConnection(connectionstring);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@tableId", tableId);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new UserRepositoryADOException("TableRepositoryADO - DeleteTable", ex);
                }
                finally
                {
                    conn.Close();
                }

            }
        }


        public Table GetTable(int tableId)
        {
            string query = "select * from Tables where TableId = @tableId and TableIsDeleted = 0;";
            SqlConnection conn = new SqlConnection(connectionstring);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@tableId", tableId);
                    IDataReader dataReader = cmd.ExecuteReader();

                    dataReader.Read();
                    
                    Table t = new Table((int)dataReader["TableId"], (int)dataReader["TableNumber"], (int)dataReader["Seats"], (int)dataReader["RestaurantId"]);
                        
                    
                    return t;
                }
                catch (Exception ex)
                {
                    throw new TableRepositoryADOException("TableRepositoryADO - GetTable", ex);
                }
                finally
                {
                    conn.Close();
                }
            }

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
            string query = @"Select count(*) from Tables where TableId=@tableId AND TableIsDeleted = 0";
            SqlConnection conn = new SqlConnection(connectionstring);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@tableId", tableId);
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

        public void UpdateTable(Table table)
        {
            string query = @"UPDATE Tables SET TableNumber=@tablenumber, Seats=@seats WHERE TableId=@tableId";
            SqlConnection conn = new SqlConnection(connectionstring);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@tablenumber", table.TableNumber);
                    cmd.Parameters.AddWithValue("@seats", table.Seats);
                    cmd.Parameters.AddWithValue("@tableId", table.TableId);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new UserRepositoryADOException("TableRepositoryADO - UpdateTable", ex);
                }
                finally
                {
                    conn.Close();
                }

            }
        }
    }
}
