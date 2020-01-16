using ModelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Models.ViewModels
{
    public class AanvalViewModel
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public int MaxPP { get; set; }
        public int PP { get; set; }
        public int Accuratie { get; set; }
        public int Power { get; set; }
        public SoortViewModel Soort { get; set; }
    }
}
