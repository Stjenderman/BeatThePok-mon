using BeatThePokemon.Models.ViewModels;
using ModelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Models.Convert
{
    public class AanvalViewModelConvert
    {
        static public Aanval ViewModelToAanval(AanvalViewModel avm)
        {
            return new Aanval(avm.Id, avm.Naam, avm.MaxPP, avm.PP, avm.Accuratie, avm.Power, SoortViewModelConvert.ViewModelToSoort(avm.Soort));
        }

        static public AanvalViewModel AanvalToViewModel(Aanval a)
        {
            AanvalViewModel avm = new AanvalViewModel();
            avm.Id = a.Id;
            avm.Naam = a.Naam;
            avm.MaxPP = a.MaxPP;
            avm.PP = a.PP;
            avm.Accuratie = a.Accuratie;
            avm.Power = a.Power;
            avm.Soort = SoortViewModelConvert.SoortToViewModel(a.Soort);

            return avm;
        }

        static public List<AanvalViewModel> AanvalListToViewModelList(List<Aanval> a)
        {
            List<AanvalViewModel> avm = new List<AanvalViewModel>();
            foreach (var aanval in a)
            {
                avm.Add(AanvalToViewModel(aanval));
            }

            return avm;
        }
    }
}
