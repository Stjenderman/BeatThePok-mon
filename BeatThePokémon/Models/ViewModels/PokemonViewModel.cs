using ModelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Models.ViewModels
{
    public class PokemonViewModel
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public string Naam { get; set; }
        public SoortViewModel Type { get; set; }
        public HpValues MaxHP { get; set; }
        public int HP { get; set; }
        public byte[] Uiterlijk { get; set; }
        public List<AanvalViewModel> Aanvallen { get; set; }
        public string Image { get; set; }
    }
}
