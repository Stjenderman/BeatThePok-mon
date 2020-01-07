using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Gebruikersnaam { get; set; }
        public string Wachtwoord { get; set; }
        public string Email { get; set; }
        public List<Pokemon> Pokemon { get; set; }

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
