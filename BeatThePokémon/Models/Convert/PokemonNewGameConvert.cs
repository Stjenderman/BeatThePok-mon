using BeatThePokemon.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Models.Convert
{
    public class PokemonNewGameConvert
    {
        public NewGameViewModel PokemonToNewGame(List<Pokemon> pokemon)
        {
            NewGameViewModel ngvm = new NewGameViewModel();
            List<NewGameViewModel> viewModels = new List<NewGameViewModel>();
            foreach (Pokemon tempPok in pokemon)
            {
                NewGameViewModel temp = new NewGameViewModel();
                temp.Pokemon = tempPok;
                temp.Image = System.Convert.ToBase64String(tempPok.Uiterlijk);
                viewModels.Add(temp);
            }
            ngvm.RandomPokemon = viewModels.ToArray();
            return ngvm;
        }
    }
}
