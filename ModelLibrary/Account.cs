using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelLibrary.Models
{
    public class Account
    {
        public int Id { get; }
        public string Gebruikersnaam { get; set; }
        public string Wachtwoord { get; set; }
        public string Email { get; set; }
        public List<Pokemon> Pokemon { get; set; }
        public Tegenstander Tegenstander { get; set; }

        public Account(int id, string gebruikersnaam, string wachtwoord, string email, List<Pokemon> pokemon)
        {
            this.Id = id;
            this.Gebruikersnaam = gebruikersnaam;
            this.Wachtwoord = wachtwoord;
            this.Email = email;
            this.Pokemon = pokemon;
        }

        public Account()
        {

        }
    }
}
