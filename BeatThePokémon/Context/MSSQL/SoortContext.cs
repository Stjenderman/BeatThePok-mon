using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeatThePokemon.Context.Interfaces;
using BeatThePokemon.Models;
using Microsoft.Data.SqlClient;

namespace BeatThePokemon.Context.MSSQL
{
    public class SoortContext : DataCon, ISoortContext
    {
        public List<Soort> GetAll()
        {
            List<Soort> soorten = new List<Soort>();
            string query = "SELECT * FROM dbo.Soort";

            try
            {
                SqlCommand cmd = new SqlCommand(query, OpenConn());
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Soort tempSoort = new Soort((Soort.TypeSoorten)reader["Naam"], reader["ImageNaam"].ToString());
                        soorten.Add(tempSoort);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return soorten;
        }
    }
}
