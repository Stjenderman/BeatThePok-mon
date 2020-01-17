using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelLibrary.Models;

namespace BeatThePokemon.Models.ViewModels
{
    public class GevechtViewModel
    {
        public PokemonViewModel GebruikerPokemon { get; set; }
        public PokemonViewModel TegenstanderPokemon { get; set; }
        public int OudGebruikerHp { get; set; }
        public int OudtegenstanderHp { get; set; }

        public GevechtViewModel()
        {
            
        }
    }
}
