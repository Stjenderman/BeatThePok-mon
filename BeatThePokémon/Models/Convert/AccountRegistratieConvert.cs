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
            Account a = new Account(rvm.Gebruikersnaam, rvm.Wachtwoord, rvm.Email);
            return a;
        }
    }
}
