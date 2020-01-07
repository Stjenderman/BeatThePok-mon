using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Models.ViewModels
{
    public class NewGameViewModel
    {
        public Pokemon Pokemon { get; set; }
        public string Image { get; set; }
        public NewGameViewModel[] RandomPokemon { get; set; }
        public int PokemonId { get; set; }


        public NewGameViewModel()
        {
            RandomPokemon = new NewGameViewModel[3];
        }
    }
}
