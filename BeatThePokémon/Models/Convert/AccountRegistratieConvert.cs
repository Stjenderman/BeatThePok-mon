using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeatThePokemon.Models.ViewModels;

namespace BeatThePokemon.Models.Convert
{
    public class AccountRegistratieConvert
    {
        public Account RegistratieToAccount(RegistratieViewModel rvm)
        {
            Account a = new Account()
            {
                Gebruikersnaam = rvm.Gebruikersnaam,
                Wachtwoord = rvm.Wachtwoord,
                Email = rvm.Email
            };

            return a;
        }
    }
}
