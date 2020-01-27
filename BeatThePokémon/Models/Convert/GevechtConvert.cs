using BeatThePokemon.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelLibrary.Models;

namespace BeatThePokemon.Models.Convert
{
    public class GevechtConvert
    {
        static public GevechtViewModel ImageConvert(GevechtViewModel gvm)
        {
            gvm.GebruikerPokemon.Image = System.Convert.ToBase64String(gvm.GebruikerPokemon.Uiterlijk);
            gvm.TegenstanderPokemon.Image = System.Convert.ToBase64String(gvm.TegenstanderPokemon.Uiterlijk);

            return gvm;
        }
    }
}
