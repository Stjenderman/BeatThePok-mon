﻿using BeatThePokemon.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelLibrary.Models;

namespace BeatThePokemon.Models.Convert
{
    public class GevechtConvert
    {
        public GevechtViewModel AccountToGevecht(Pokemon gebruikerPokemon, Pokemon tegenstanderPokemon)
        {
            GevechtViewModel gvm = new GevechtViewModel();
            gebruikerPokemon.Image = System.Convert.ToBase64String(gebruikerPokemon.Uiterlijk);
            tegenstanderPokemon.Image = System.Convert.ToBase64String(tegenstanderPokemon.Uiterlijk);

            gvm.GebruikerPokemon = PokemonViewModelConvert.PokemonToPokemonViewModel(gebruikerPokemon);
            gvm.TegenstanderPokemon = PokemonViewModelConvert.PokemonToPokemonViewModel(tegenstanderPokemon);

            return gvm;
        }
    }
}
