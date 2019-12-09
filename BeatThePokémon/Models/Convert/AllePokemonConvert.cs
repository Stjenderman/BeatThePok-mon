using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeatThePokemon.Models.ViewModels;

namespace BeatThePokemon.Models.Convert
{
    public class AllePokemonConvert
    {
        public AllePokemonViewModel PokemonToView(List<Pokemon> pList)
        {
            AllePokemonViewModel pvvm = new AllePokemonViewModel();
            List<AllePokemonViewModel> temp = new List<AllePokemonViewModel>();
            foreach (Pokemon tempPok in pList)
            {
                AllePokemonViewModel pvvmTemp = new AllePokemonViewModel();
                pvvmTemp.Id = tempPok.Id;
                pvvmTemp.Naam = tempPok.Naam;
                pvvmTemp.Type = tempPok.Type;
                pvvmTemp.Image = System.Convert.ToBase64String(tempPok.Uiterlijk);
                temp.Add(pvvmTemp);
            }

            pvvm.AllePokemon = temp;
            return pvvm;
        }
    }
}
