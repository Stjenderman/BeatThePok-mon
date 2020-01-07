using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Models.ViewModels
{
    public class AllePokemonViewModel
    {
        public string Naam { get; set; }
        public Soort Type { get; set; }
        public string Image { get; set; }
        public List<Aanval> Aanvallen { get; set; }

        public List<AllePokemonViewModel> AllePokemon;
        
        public int Id { get; set; }
    }
}
