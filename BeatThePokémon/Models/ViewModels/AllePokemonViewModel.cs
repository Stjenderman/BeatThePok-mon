﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelLibrary.Models;

namespace BeatThePokemon.Models.ViewModels
{
    public class AllePokemonViewModel
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public SoortViewModel Type { get; set; }
        public string Image { get; set; }
        public List<AanvalViewModel> Aanvallen { get; set; }
        public List<AllePokemonViewModel> AllePokemon { get; set; }
    }
}
