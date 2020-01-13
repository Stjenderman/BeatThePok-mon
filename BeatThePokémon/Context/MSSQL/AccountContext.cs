using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BeatThePokemon.Context.Interfaces;
using BeatThePokemon.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BeatThePokemon.Context.MSSQL
{
    public class AccountContext : IAccountContext
    {
        private readonly string _connstring;
        private IConfiguration iConfig;

        public AccountContext(IConfiguration configuration)
        {
            _connstring = configuration.GetConnectionString("DefaultConnection");
            iConfig = configuration;
        }

        public bool Create(Account account)
        {
            string query = "INSERT INTO dbo.Gebruiker (Gebruikersnaam, Wachtwoord, Email) VALUES (@Gebruikersnaam, @Wachtwoord, @Email)";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connstring))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    using (cmd)
                    {
                        cmd.Parameters.AddWithValue(@"Gebruikersnaam", account.Gebruikersnaam);
                        cmd.Parameters.AddWithValue(@"Wachtwoord", account.Wachtwoord);
                        cmd.Parameters.AddWithValue(@"Email", account.Email);
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

        public Account GetById(int id)
        {
            string query = "SELECT * FROM dbo.Gebruiker WHERE Id = @id";
            Account a = new Account();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connstring))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    using (cmd)
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                a = new Account((int)reader["Id"], reader["Gebruikersnaam"].ToString(), reader["Wachtwoord"].ToString(), reader["Email"].ToString(), null);
                            }
                        }
                    }
                    conn.Close();
                }
                a.Pokemon = GetAllPokemonOfUser(a.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return a;
        }

        public Account GetByNaam(string naam)
        {
            string query = "SELECT * FROM dbo.Gebruiker WHERE Gebruikersnaam = @naam";
            Account a = new Account();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connstring))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    using (cmd)
                    {
                        cmd.Parameters.AddWithValue("@naam", naam);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            reader.Read();
                            a = new Account((int)reader["Id"], reader["Gebruikersnaam"].ToString(), reader["Wachtwoord"].ToString(), reader["Email"].ToString(), null);
                        }
                    }
                    conn.Close();
                }
                a.Pokemon = GetAllPokemonOfUser(a.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return a;
        }

        public List<Pokemon> GetAllPokemonOfUser(int id)
        {
            string query =
                "SELECT * FROM dbo.Pokémon p INNER JOIN dbo.Soort s ON p.Soort = s.NaamId INNER JOIN dbo.GebruikerPokémon gb ON p.Id = gb.PokémonId WHERE gb.GebruikerId = @Id";

            List<Pokemon> pokemonList = new List<Pokemon>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connstring))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    using (cmd)
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                    }
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Soort s = new Soort((Soort.TypeSoorten)reader["NaamId"], reader["ImageNaam"].ToString());
                            Pokemon p = new Pokemon((int)reader["Id"], (int)reader["GebruikerTeamId"], reader["Naam"].ToString(), s, null, (HpValues)reader["MaxHP"], (int)reader["HP"], (byte[])reader["Uiterlijk"]);
                            pokemonList.Add(p);
                        }
                    }
                    conn.Close();

                    AanvalContext ac = new AanvalContext(iConfig);
                    foreach (Pokemon p in pokemonList)
                    {
                        p.Aanvallen = ac.GetAllByPokemon(p.Id);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return pokemonList;
        }
    }
}
