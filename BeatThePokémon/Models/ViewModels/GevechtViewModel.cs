using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Models.ViewModels
{
    public class GevechtViewModel
    {
        public List<GevechtViewModel> AccountPokemon { get; set; }
        public Pokemon Pokemon { get; set; }
        public string Image { get; set; }

        public GevechtViewModel()
        {
            AccountPokemon = new List<GevechtViewModel>();
        }
    }
}
