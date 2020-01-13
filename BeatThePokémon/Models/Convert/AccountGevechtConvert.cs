using BeatThePokemon.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Models.Convert
{
    public class AccountGevechtConvert
    {
        public GevechtViewModel AccountToGevecht(Pokemon gebruikerPokemon, Pokemon tegenstanderPokemon)
        {
            GevechtViewModel gvm = new GevechtViewModel();
            gebruikerPokemon.Image = System.Convert.ToBase64String(gebruikerPokemon.Uiterlijk);
            tegenstanderPokemon.Image = System.Convert.ToBase64String(tegenstanderPokemon.Uiterlijk);

            gvm.GebruikerPokemon = gebruikerPokemon;
            gvm.TegenstanderPokemon = tegenstanderPokemon;

            return gvm;
        }
    }
}
