using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ModelLibrary.Models.Soort;

namespace BeatThePokemon.Models.ViewModels
{
    public class SoortViewModel
    {
        public TypeSoorten Naam { get; set; }
        public string ImageNaam { get; set; }
    }
}
