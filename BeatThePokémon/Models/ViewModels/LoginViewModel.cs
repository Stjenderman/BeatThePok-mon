using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Voer een gebruikersnaam in!")]
        public string Gebruikersnaam { get; set; }
        [Required(ErrorMessage = "Voer een Wachtwoord in!")]
        public string Wachtwoord { get; set; }
    }
}