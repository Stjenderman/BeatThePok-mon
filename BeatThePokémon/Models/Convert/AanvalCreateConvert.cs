using BeatThePokemon.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelLibrary.Models;

namespace BeatThePokemon.Models.Convert
{
    public class AanvalCreateConvert
    {
        public Aanval CreateToAanval(AanvalCreateViewModel acvm)
        {
            Soort s = new Soort((Soort.TypeSoorten)acvm.Soort, null);
            return new Aanval(-1, acvm.Naam, acvm.PP, acvm.Accuratie, acvm.Power, s);
        }
    }
}
