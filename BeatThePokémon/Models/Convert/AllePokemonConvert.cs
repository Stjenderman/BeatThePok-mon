using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeatThePokemon.Models.ViewModels;
using ModelLibrary.Models;

namespace BeatThePokemon.Models.Convert
{
    public class AllePokemonConvert
    {
        public AllePokemonViewModel PokemonToView(List<Pokemon> pList)
        {
            AllePokemonViewModel apvm = new AllePokemonViewModel();
            List<AllePokemonViewModel> temp = new List<AllePokemonViewModel>();
            foreach (Pokemon p in pList)
            {
                AllePokemonViewModel apvmTemp = new AllePokemonViewModel();
                apvmTemp.Id = p.Id;
                apvmTemp.Naam = p.Naam;
                apvmTemp.Type = p.Type;
                apvmTemp.Image = System.Convert.ToBase64String(p.Uiterlijk);
                apvmTemp.Aanvallen = p.Aanvallen;
                temp.Add(apvmTemp);
            }

            apvm.AllePokemon = temp;
            return apvm;
        }
    }
}
