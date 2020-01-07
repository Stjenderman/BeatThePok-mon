using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BeatThePokemon.Models.ViewModels
{
    public class PokemonCreateViewModel
    {
        [Required(ErrorMessage = "Vul een naam in!")]
        [DataType(DataType.Text)]
        public string Naam { get; set; }
        [Required(ErrorMessage = "Selecteer een soort!")]
        public int Type { get; set; }
        [Required(ErrorMessage = "Voeg een afbeelding toe!")]
        public IFormFile Image { get; set; }
        public string NaamAanval { get; set; }
        public int MaxHP { get; set; }
        public Aanval ToeTeVoegenAanval { get; set; }
        public List<Soort> AlleSoorten { get; set; }
    }
}
