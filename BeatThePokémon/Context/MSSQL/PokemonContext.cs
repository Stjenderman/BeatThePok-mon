using BeatThePokemon.Models;
using BeatThePokemon.Models.ViewModels;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BeatThePokemon.Context.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BeatThePokemon.Context.MSSQL
{
    public class PokemonContext : IPokemonContext
    {
        private readonly string _connstring;
        private IConfiguration iConfig;

        public PokemonContext(IConfiguration configuration)
        {
            _connstring = configuration.GetConnectionString("DefaultConnection");
            iConfig = configuration;
        }

        public bool Create(Pokemon pokemon)
        {
            string query = "INSERT INTO dbo.Pokémon (Naam, Soort, Uiterlijk) OUTPUT INSERTED.Id VALUES (@Naam, @Soort, @Uiterlijk)";
            int pokemonId = -1;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connstring))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    using (cmd)
                    {
                        cmd.Parameters.AddWithValue(@"Naam", pokemon.Naam);
                        cmd.Parameters.AddWithValue(@"Soort", (int)pokemon.Type.Naam);
                        cmd.Parameters.Add("@Uiterlijk", SqlDbType.VarBinary).Value = pokemon.Uiterlijk;

                        pokemonId = (int)cmd.ExecuteScalar();
                    }

                    conn.Close();


                    foreach (Aanval aanval in pokemon.Aanvallen)
                    {
                        LinkAanvalToPokemon(pokemonId, aanval.Id, aanval.MaxPP);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private bool LinkAanvalToPokemon(int pokId, int aanId, int maxPP)
        {
            string query = "INSERT INTO dbo.AanvalPokémon (PokémonId, AanvalId, PP) VALUES (@PokémonId, @AanvalId, @PP)";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connstring))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    using (cmd)
                    {
                        cmd.Parameters.AddWithValue(@"PokémonId", pokId);
                        cmd.Parameters.AddWithValue(@"AanvalId", aanId);
                        cmd.Parameters.AddWithValue(@"PP", maxPP);
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

        public Pokemon GetById(int id)
        {
            Pokemon p = new Pokemon();
            string query = 
                "SELECT * " +
                "FROM Pokémon p " +
                "INNER JOIN Soort ps " +
                "ON p.Soort = ps.NaamId " +
                "WHERE p.Id = @Id";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connstring))
                {
                    conn.Open();
                    SqlCommand pokcmd = new SqlCommand(query, conn);

                    pokcmd.Parameters.AddWithValue("@Id", id);


                    using (SqlDataReader reader = pokcmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Soort s = new Soort((Soort.TypeSoorten)reader["NaamId"], reader["ImageNaam"].ToString());
                            p = new Pokemon((int)reader["Id"], reader["Naam"].ToString(), s, null, (byte[])reader["Uiterlijk"]);
                        }
                    }
                    conn.Close();

                    AanvalContext a = new AanvalContext(iConfig);
                    p.Aanvallen = a.GetAllByPokemon(p.Id);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return p;
        }

        public List<Pokemon> GetAll()
        {
            List<Pokemon> pokemonList = new List<Pokemon>();
            string query =
                "SELECT * " +
                "FROM Pokémon p " +
                "INNER JOIN Soort ps " +
                "ON p.Soort = ps.NaamId";

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
                            Pokemon p = new Pokemon((int)reader["Id"], reader["Naam"].ToString(), s, null, (byte[])reader["Uiterlijk"]);
                            pokemonList.Add(p);
                        }
                    }
                    conn.Close();
                }

                AanvalContext ac = new AanvalContext(iConfig);
                foreach (Pokemon p in pokemonList)
                {
                    p.Aanvallen = ac.GetAllByPokemon(p.Id);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return pokemonList;
        }

        public bool Delete(int id)
        {
            if(DeletePokemon(id))
            {
                if(DeleteAanvalOfPokemon(id))
                {
                    return true;
                }
            }
            return false;
        }

        private bool DeletePokemon(int id)
        {
            string query = "DELETE FROM dbo.Pokémon WHERE Id = @Id";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connstring))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);

                    using (cmd)
                    {
                        cmd.Parameters.AddWithValue(@"Id", id);
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

        private bool DeleteAanvalOfPokemon(int id)
        {
            string query = "DELETE FROM dbo.AanvalPokémon WHERE Id = @Id";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connstring))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);

                    using (cmd)
                    {
                        cmd.Parameters.AddWithValue(@"Id", id);
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
