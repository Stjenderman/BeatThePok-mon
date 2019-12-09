using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BeatThePokemon.Context.Interfaces;
using BeatThePokemon.Models;
using Microsoft.Data.SqlClient;

namespace BeatThePokemon.Context.MSSQL
{
    public class AccountContext : DataCon, IAccountContext
    {
        public bool Create( Account account)
        {
            string query = "INSERT INTO dbo.Gebruiker (Gebruikersnaam, Wachtwoord, Email) VALUES (@Gebruikersnaam, @Wachtwoord, @Email)";

            try
            {
                SqlCommand cmd = new SqlCommand(query, OpenConn());
                using (cmd)
                {
                    cmd.Parameters.AddWithValue(@"Gebruikersnaam", account.Gebruikersnaam);
                    cmd.Parameters.AddWithValue(@"Wachtwoord", account.Wachtwoord);
                    cmd.Parameters.AddWithValue(@"Email", account.Email);
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

        public Account GetByNaam(string naam)
        {
            string query = "SELECT * FROM dbo.Gebruiker WHERE Gebruikersnaam = @naam";
            Account a = new Account();

            try
            {
                SqlCommand cmd = new SqlCommand(query, OpenConn());
                using(cmd)
                {
                    cmd.Parameters.AddWithValue("@naam", naam);
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            a = new Account(reader["Gebruikersnaam"].ToString(), reader["Wachtwoord"].ToString(), reader["Email"].ToString());
                        }
                    }
                }
                CloseConn();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return a;
        }
    }
}
