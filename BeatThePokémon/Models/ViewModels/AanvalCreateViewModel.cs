using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ModelLibrary.Models;

namespace BeatThePokemon.Models.ViewModels
{
    public class AanvalCreateViewModel
    {
        [Required(ErrorMessage = "Vul een naam in!")]
        public string Naam { get; set; }
        [Required(ErrorMessage = "Vul een pp in!")]
        public int PP { get; set; }
        [Required(ErrorMessage = "Vul een accuratie in!")]
        public int Accuratie { get; set; }
        [Required(ErrorMessage = "Vul een power in!")]
        public int Power { get; set; }
        [Required(ErrorMessage = "Kies een soort!")]
        public int Soort { get; set; }
        public List<SoortViewModel> AlleSoorten { get; set; }
    }
}
