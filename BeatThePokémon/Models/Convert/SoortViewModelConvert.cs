using BeatThePokemon.Models.ViewModels;
using ModelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Models.Convert
{
    public class SoortViewModelConvert
    {
        static public Soort ViewModelToSoort(SoortViewModel svm)
        {
            return new Soort(svm.Naam, svm.ImageNaam);
        }

        static public SoortViewModel SoortToViewModel(Soort s)
        {
            SoortViewModel svm = new SoortViewModel();
            svm.Naam = s.Naam;
            svm.ImageNaam = s.ImageNaam;

            return svm;
        }

        static public List<SoortViewModel> SoortListToViewModelList(List<Soort> s)
        {
            List<SoortViewModel> svm = new List<SoortViewModel>();
            foreach (var soort in s)
            {
                svm.Add(SoortToViewModel(soort));
            }
            return svm;
        }
    }
}
