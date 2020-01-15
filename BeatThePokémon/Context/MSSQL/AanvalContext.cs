using BeatThePokemon.Context.Interfaces;
using ModelLibrary.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Context.MSSQL
{
    public class AanvalContext : IAanvalContext
    {
        private readonly string _connstring;

        public AanvalContext(IConfiguration configuration)
        {
            _connstring = configuration.GetConnectionString("DefaultConnection");
        }

        public bool Create(Aanval aanval)
        {
            string query = "INSERT INTO dbo.Aanval (Naam, MaxPP, Accuratie, Power, Soort) VALUES (@Naam, @MaxPP, @Accuratie, @Power, @Soort)";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connstring))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    using (cmd)
                    {
                        cmd.Parameters.AddWithValue(@"Naam", aanval.Naam);
                        cmd.Parameters.AddWithValue(@"MaxPP", aanval.MaxPP);
                        cmd.Parameters.AddWithValue(@"Accuratie", aanval.Accuratie);
                        cmd.Parameters.AddWithValue(@"Power", aanval.Power);
                        cmd.Parameters.AddWithValue(@"Soort", (int)aanval.Soort.Naam);
                    }

                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<Aanval> GetAllByPokemon(int id)
        {
            List<Aanval> aanvalList = new List<Aanval>();
            string query = 
                "SELECT * " +
                "FROM AanvalPokémon ap " +
                "INNER JOIN Aanval a " +
                "ON ap.AanvalId = a.Id " +
                "INNER JOIN Soort soa " +
                "ON a.Soort = soa.NaamId " +
                "WHERE ap.PokémonId = @Id";

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
                            Soort s = new Soort((Soort.TypeSoorten)reader["NaamId"], reader["ImageNaam"].ToString());
                            Aanval a = new Aanval((int)reader["Id"], reader["Naam"].ToString(), (int)reader["MaxPP"], (int)reader["Accuratie"], (int)reader["Power"], s);
                            aanvalList.Add(a);
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

            return aanvalList;
        }

        public List<Aanval> GetAll()
        {
            List<Aanval> aanvalList = new List<Aanval>();
            string query = "SELECT * " +
                "FROM Aanval a " +
                "INNER JOIN Soort s " +
                "ON a.Soort = s.NaamId";

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
                            Soort s = new Soort((Soort.TypeSoorten)reader["NaamId"], reader["ImageNaam"].ToString());

                            Aanval a = new Aanval((int)reader["Id"], reader["Naam"].ToString(), (int)reader["MaxPP"], (int)reader["Accuratie"], (int)reader["Power"], s);
                            aanvalList.Add(a);
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

            return aanvalList;
        }

        public Aanval GetByName(string naam)
        {
            Aanval a = new Aanval();
            string query =
                "SELECT * " +
                "FROM Aanval a " +
                "INNER JOIN Soort soa " +
                "ON a.Soort = soa.NaamId " +
                "WHERE a.Naam = @Naam";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connstring))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Naam", naam);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Soort s = new Soort((Soort.TypeSoorten)reader["NaamId"], reader["ImageNaam"].ToString());
                            a = new Aanval((int)reader["Id"], reader["Naam"].ToString(), (int)reader["MaxPP"], (int)reader["Accuratie"], (int)reader["Power"], s);
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

            return a;
        }

        public Aanval GetById(int Id)
        {
            Aanval a = new Aanval();
            string query =
                "SELECT * " +
                "FROM Aanval a " +
                "INNER JOIN Soort soa " +
                "ON a.Soort = soa.NaamId " +
                "WHERE a.Id = @Id";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connstring))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", Id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Soort s = new Soort((Soort.TypeSoorten)reader["NaamId"], reader["ImageNaam"].ToString());
                            a = new Aanval((int)reader["Id"], reader["Naam"].ToString(), (int)reader["MaxPP"], (int)reader["Accuratie"], (int)reader["Power"], s);
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

            return a;
        }
    }
}
