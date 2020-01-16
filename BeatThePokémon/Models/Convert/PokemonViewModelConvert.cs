using BeatThePokemon.Models.ViewModels;
using ModelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Models.Convert
{
    public class PokemonViewModelConvert
    {
        static public PokemonViewModel PokemonToPokemonViewModel(Pokemon p)
        {
            PokemonViewModel pvm = new PokemonViewModel();
            pvm.Id = p.Id;
            pvm.HP = p.HP;
            pvm.Image = p.Image;
            pvm.MaxHP = p.MaxHP;
            pvm.Naam = p.Naam;
            pvm.TeamId = p.TeamId;
            pvm.Type = SoortViewModelConvert.SoortToViewModel(p.Type);
            pvm.Uiterlijk = p.Uiterlijk;
            pvm.Aanvallen = AanvalViewModelConvert.AanvalListToViewModelList(p.Aanvallen);            

            return pvm;
        }
    }
}
