using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Models.ViewModels
{
    public class TegenstanderViewModel
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public List<PokemonViewModel> Pokemon { get; set; }
    }
}
