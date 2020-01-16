using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Models.ViewModels
{
    public class AccountViewModel
    {
        public int Id { get; set; }
        public string Gebruikersnaam { get; set; }
        public string Wachtwoord { get; set; }
        public string Email { get; set; }
        public List<PokemonViewModel> Pokemon { get; set; }
        public TegenstanderViewModel Tegenstander { get; set; }
    }
}
