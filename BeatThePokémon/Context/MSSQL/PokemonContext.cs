using BeatThePokemon.Models;
using BeatThePokemon.Models.ViewModels;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BeatThePokemon.Context.Interfaces;

namespace BeatThePokemon.Context.MSSQL
{
    public class PokemonContext : DataCon, IPokemonContext
    {
        public bool Create(Pokemon pokemon)
        {
            string query = "INSERT INTO dbo.Pokémon (Naam, Soort, Uiterlijk) VALUES (@Naam, @Soort, @Uiterlijk)";

            try
            {
                SqlCommand cmd = new SqlCommand(query, OpenConn());
                using (cmd)
                {
                    cmd.Parameters.AddWithValue(@"Naam", pokemon.Naam);
                    cmd.Parameters.AddWithValue(@"Soort", (int)pokemon.Type.Naam);
                    cmd.Parameters.Add("@Uiterlijk", SqlDbType.VarBinary).Value = pokemon.Uiterlijk;
                }

                cmd.ExecuteNonQuery();

                CloseConn();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public List<Pokemon> GetAll()
        {
            List<Pokemon> pokémonList = new List<Pokemon>();
            string query = "SELECT * FROM dbo.Pokémon INNER JOIN dbo.Soort ON dbo.Pokémon.Soort = dbo.Soort.Naam";

            try
            {
                SqlCommand cmd = new SqlCommand(query, OpenConn());
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Soort tempType = new Soort(((Soort.TypeSoorten)reader["Soort"]), reader["ImageNaam"].ToString());

                        Pokemon p = new Pokemon((int)reader["Id"],reader["Naam"].ToString(), tempType, (byte[]) reader["Uiterlijk"]);
                        pokémonList.Add(p);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return pokémonList;
        }

        public bool Delete(int id)
        {
            string query = "DELETE FROM dbo.Pokémon WHERE Id = @Id";
            try
            {
                SqlCommand cmd = new SqlCommand(query, OpenConn());

                using (cmd)
                {
                    cmd.Parameters.AddWithValue(@"Id", id);
                }

                cmd.ExecuteNonQuery();
                CloseConn();
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
