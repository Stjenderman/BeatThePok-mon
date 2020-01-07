using BeatThePokemon.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Models.Convert
{
    public class AccountGevechtConvert
    {
        public GevechtViewModel AccountToGevecht(Account a)
        {
            GevechtViewModel listgvm = new GevechtViewModel();
            foreach (Pokemon p in a.Pokemon)
            {
                GevechtViewModel tempgvm = new GevechtViewModel();
                tempgvm.Pokemon = p;
                tempgvm.Image = System.Convert.ToBase64String(p.Uiterlijk);
                listgvm.AccountPokemon.Add(tempgvm);
            }
            return listgvm;
        }
    }
}
