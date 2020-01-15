using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeatThePokemon.Context.Interfaces;
using ModelLibrary.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BeatThePokemon.Context.MSSQL
{
    public class SoortContext : ISoortContext
    {
        private readonly string _connstring;

        public SoortContext(IConfiguration configuration)
        {
            _connstring = configuration.GetConnectionString("DefaultConnection");
        }

        public Soort GetById(int id)
        {
            Soort s = new Soort();
            string query = "SELECT * FROM dbo.Soort WHERE dbo.Soort.Naam = @Id";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connstring))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            s = new Soort((Soort.TypeSoorten)reader["Naam"], reader["ImageNaam"].ToString());
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return s;
        }

        public List<Soort> GetAll()
        {
            List<Soort> soorten = new List<Soort>();
            string query = "SELECT * FROM dbo.Soort";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connstring))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Soort tempSoort = new Soort((Soort.TypeSoorten)reader["NaamId"], reader["ImageNaam"].ToString());
                            soorten.Add(tempSoort);
                        }
                    }
                    conn.Close();
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
