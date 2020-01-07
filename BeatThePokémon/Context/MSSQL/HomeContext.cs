using BeatThePokemon.Context.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Context.MSSQL
{
    public class HomeContext : IHomeContext
    {
        private readonly string _connstring;

        public HomeContext(IConfiguration configuration)
        {
            _connstring = configuration.GetConnectionString("DefaultConnection");
        }

        public List<int> GetAllId()
        {
            string query = "SELECT Id FROM dbo.Pokémon";
            List<int> ids = new List<int>();

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
                            ids.Add((int)reader["Id"]);
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
            return ids;
        }

        public bool LinkPokemonToAccount(int pokeId, int accId, int maxHP)
        {
            string query = "INSERT INTO dbo.GebruikerPokémon (GebruikerId, PokémonId, MaxHP, Hp) VALUES (@gebruikerId, @pokémonId, @maxHP, @hp)";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connstring))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    using (cmd)
                    {
                        cmd.Parameters.AddWithValue(@"gebruikerId", accId);
                        cmd.Parameters.AddWithValue(@"pokémonId", pokeId);
                        cmd.Parameters.AddWithValue(@"maxHP", maxHP);
                        cmd.Parameters.AddWithValue(@"hp", maxHP);
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
    }
}
