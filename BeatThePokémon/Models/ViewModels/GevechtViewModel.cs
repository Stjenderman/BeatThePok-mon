using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelLibrary.Models;

namespace BeatThePokemon.Models.ViewModels
{
    public class GevechtViewModel
    {
        public Pokemon GebruikerPokemon { get; set; }
        public Pokemon TegenstanderPokemon { get; set; }

        public GevechtViewModel()
        {
            
        }
    }
}
