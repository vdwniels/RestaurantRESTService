using AdresBeheerBL.Interfaces;
using AdresBeheerBL.Model;
using AdresBeheerDL.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdresBeheerDL.repositories
{
    public class GemeenteRepositoryADO : IGemeenteRepository
    {
        private string connectionString;

        public GemeenteRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Gemeente GeefGemeente(int gemeenteId)
        {
            string query = "select * from Gemeente where NIScode=@niscode";
            SqlConnection conn = new SqlConnection(connectionString);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@niscode", gemeenteId);
                    IDataReader dataReader = cmd.ExecuteReader();
                    dataReader.Read();
                    string gemeentenaam = (string)dataReader["gemeentenaam"];
                    int niscode = (int)dataReader["NIScode"];
                    Gemeente gemeente = new Gemeente(niscode, gemeentenaam);
                    dataReader.Close();
                    return gemeente;
                }
                catch(Exception ex)
                {
                    throw new GemeenteRepositoryException("GeefGemeente", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
