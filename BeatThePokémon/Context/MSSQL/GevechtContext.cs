using BeatThePokemon.Context.Interfaces;
using BeatThePokemon.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Context.MSSQL
{
    public class GevechtContext : IGevechtContext
    {
        private readonly string _connstring;
        private IConfiguration iConfig;

        public GevechtContext(IConfiguration configuration)
        {
            _connstring = configuration.GetConnectionString("DefaultConnection");
            iConfig = configuration;
        }

        public List<int> GetAllId()
        {
            string query =
                "SELECT Id " +
                "FROM dbo.Tegenstander";

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

        public Tegenstander GetById(int id, int gebruikerId)
        {
            Tegenstander t = new Tegenstander();
            string query =
                "SELECT * " +
                "FROM dbo.Tegenstander " +
                "WHERE Id = @Id";

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
                            t = new Tegenstander((int)reader["Id"], reader["Naam"].ToString(), null);
                        }
                    }
                    conn.Close();

                    t.Pokemon = GetAllPokemonOfTegenstander(t.Id, gebruikerId);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return t;
        }

        public bool LinkTegenstanderAanGevecht(int tegenstanderId, int accountId, List<Pokemon> pokemon)
        {
            string query =
                "INSERT INTO dbo.TegenstanderGebruikerPokemon " +
                "(TegenstanderId, GebruikerId, PokemonId, MaxHP, HP) " +
                "VALUES (@TegenstanderId, @GebruikerId, @PokemonId, @MaxHP, @HP)";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connstring))
                {
                    foreach (Pokemon p in pokemon)
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@TegenstanderId", tegenstanderId);
                            cmd.Parameters.AddWithValue("@GebruikerId", accountId);
                            cmd.Parameters.AddWithValue("@PokemonId", p.Id);
                            cmd.Parameters.AddWithValue("@MaxHP", p.MaxHP);
                            cmd.Parameters.AddWithValue("@HP", p.MaxHP);

                            cmd.ExecuteNonQuery();
                        }
                        conn.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return true;
        }

        public List<Pokemon> GetAllPokemonOfTegenstander(int tegenstanderId, int gebruikerId)
        {
            string query =
                "SELECT * FROM dbo.Pokémon p " +
                "INNER JOIN dbo.TegenstanderGebruikerPokemon tgp ON p.Id = tgp.PokemonId " +
                "INNER JOIN dbo.Soort s ON p.Soort = s.NaamId " +
                "WHERE tgp.TegenstanderId = @TegenstanderId " +
                "AND tgp.GebruikerId = @GebruikerId";

            List<Pokemon> pokemonList = new List<Pokemon>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connstring))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    using (cmd)
                    {
                        cmd.Parameters.AddWithValue("@TegenstanderId", tegenstanderId);
                        cmd.Parameters.AddWithValue("@GebruikerId", gebruikerId);
                    }
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Soort s = new Soort((Soort.TypeSoorten)reader["NaamId"], reader["ImageNaam"].ToString());
                            Pokemon p = new Pokemon((int)reader["Id"], (int)reader["TegenstanderTeamId"], reader["Naam"].ToString(), s, null, (HpValues)reader["MaxHP"], (int)reader["HP"], (byte[])reader["Uiterlijk"]);
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

        public int GetTegenstanderIdWithUserId(int userId)
        {
            string query = "SELECT * FROM dbo.TegenstanderGebruikerPokemon WHERE GebruikerId = @GebruikerId";
            int tegenstanderId = -1;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connstring))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    using (cmd)
                    {
                        cmd.Parameters.AddWithValue("@GebruikerId", userId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                tegenstanderId = (int)reader["TegenstanderId"];
                            }
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

            return tegenstanderId;
        }

        public bool CreateGevecht(Account account, Tegenstander tegenstander)
        {
            string query = "INSERT INTO dbo.Gevecht " +
                "(GebruikerId, GebruikerPokemonId, TegenstanderPokemonId, GebruikerPokemonHp, TegenstanderPokemonHp) " +
                "VALUES (@GebruikerId, @GebruikerPokemonId, @TegenstanderPokemonId, @GebruikerPokemonHp, @TegenstanderPokemonHp)";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connstring))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@GebruikerId", account.Id);
                        cmd.Parameters.AddWithValue("@GebruikerPokemonId", account.Pokemon[0].TeamId);
                        cmd.Parameters.AddWithValue("@TegenstanderPokemonId", tegenstander.Pokemon[0].TeamId);
                        cmd.Parameters.AddWithValue("@GebruikerPokemonHp", account.Pokemon[0].MaxHP);
                        cmd.Parameters.AddWithValue("@TegenstanderPokemonHp", tegenstander.Pokemon[0].MaxHP);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return true;
        }

        public int GetTegenstanderTeamId(int gebruikerId)
        {
            string query = "SELECT TegenstanderTeamId FROM dbo.TegenstanderGebruikerPokemon WHERE GebruikerId = @GebruikerId";
            int teamId = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connstring))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@GebruikerId", gebruikerId);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            teamId = (int)reader["TegenstanderTeamId"];
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

            return teamId;
        }

        public int GetGebruikerTeamId(int gebruikerId)
        {
            string query = "SELECT GebruikerTeamId FROM dbo.GebruikerPokémon WHERE GebruikerId = @GebruikerId";
            int teamId = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connstring))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@GebruikerId", gebruikerId);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            teamId = (int)reader["GebruikerTeamId"];
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

            return teamId;
        }

        public Pokemon GetGebruikerPokemonByTeamId(int teamId)
        {
            string query = "SELECT * FROM dbo.GebruikerPokémon gb INNER JOIN dbo.Pokémon p ON gb.PokémonId = p.Id INNER JOIN dbo.Soort s ON p.Soort = s.NaamId WHERE gb.GebruikerTeamId = @TeamId";
            Pokemon p = new Pokemon();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connstring))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@TeamId", teamId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Soort s = new Soort((Soort.TypeSoorten)reader["NaamId"], reader["ImageNaam"].ToString());
                            p = new Pokemon((int)reader["Id"], (int)reader["GebruikerTeamId"], reader["Naam"].ToString(), s, null, (HpValues)reader["MaxHP"], (int)reader["HP"], (byte[])reader["Uiterlijk"]);
                        }
                    }
                    conn.Close();

                    AanvalContext ac = new AanvalContext(iConfig);
                    p.Aanvallen = ac.GetAllByPokemon(p.Id);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return p;
        }

        public Pokemon GetTegenstanderPokemonByTeamId(int teamId)
        {
            string query = "SELECT * FROM dbo.TegenstanderGebruikerPokemon gb INNER JOIN dbo.Pokémon p ON gb.PokemonId = p.Id INNER JOIN dbo.Soort s ON p.Soort = s.NaamId WHERE gb.TegenstanderTeamId = @TeamId";
            Pokemon p = new Pokemon();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connstring))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@TeamId", teamId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Soort s = new Soort((Soort.TypeSoorten)reader["NaamId"], reader["ImageNaam"].ToString());
                            p = new Pokemon((int)reader["Id"], (int)reader["TegenstanderTeamId"], reader["Naam"].ToString(), s, null, (HpValues)reader["MaxHP"], (int)reader["HP"], (byte[])reader["Uiterlijk"]);
                        }
                    }
                    conn.Close();

                    AanvalContext ac = new AanvalContext(iConfig);
                    p.Aanvallen = ac.GetAllByPokemon(p.Id);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return p;
        }

        public int GetTegenstanderPokemonHpByTeamId(int teamId)
        {
            int hp = -1;
            string query =
                "SELECT HP " +
                "FROM dbo.TegenstanderGebruikerPokemon " +
                "WHERE TegenstanderTeamId = @TeamId";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connstring))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@TeamId", teamId);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            hp = (int)reader["HP"];
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

            return hp;
        }

        public bool UpdateHpTegenstander(int nieuwHp, int teamId)
        {
            string query = "UPDATE dbo.TegenstanderGebruikerPokemon SET HP = @HP WHERE TegenstanderTeamId = @TeamId";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connstring))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@HP", nieuwHp);
                        cmd.Parameters.AddWithValue("@TeamId", teamId);

                        cmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return true;
        }

        public bool UpdateHpGebruiker(int nieuwHp, int teamId)
        {
            string query = "UPDATE dbo.GebruikerPokémon SET HP = @HP WHERE GebruikerTeamId = @TeamId";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connstring))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@HP", nieuwHp);
                        cmd.Parameters.AddWithValue("@TeamId", teamId);

                        cmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return true;
        }

        public bool GevechtExists(int gebruikerId)
        {
            string query = "SELECT * FROM dbo.Gevecht WHERE GebruikerId = @GebruikerId";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connstring))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@GebruikerId", gebruikerId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool DeleteAllNewGameData(int gebruikerId)
        {
            if (DeleteAllGevechtData(gebruikerId) && DeleteAllTegenstanderData(gebruikerId))
            {
                return true;
            }
            return false;
        }

        private bool DeleteAllGevechtData(int gebruikerId)
        {
            string query = "DELETE FROM dbo.Gevecht WHERE GebruikerId = @GebruikerId";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connstring))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@GebruikerId", gebruikerId);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return true;
        }

        private bool DeleteAllTegenstanderData(int gebruikerId)
        {
            string query = "DELETE FROM dbo.TegenstanderGebruikerPokemon WHERE GebruikerId = @GebruikerId";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connstring))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@GebruikerId", gebruikerId);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return true;
        }

        public bool RestoreAllHpOfGebruiker(List<Pokemon> gebruikerPokemon, int gebruikerId)
        {
            string query = "UPDATE dbo.GebruikerPokémon SET HP = @HP WHERE GebruikerId = @GebruikerId AND PokémonId = @PokemonId";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connstring))
                {
                    foreach (var p in gebruikerPokemon)
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@HP", p.MaxHP);
                            cmd.Parameters.AddWithValue("@GebruikerId", gebruikerId);
                            cmd.Parameters.AddWithValue("@PokemonId", p.Id);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return true;
        }
    }
}
