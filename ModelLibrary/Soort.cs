using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelLibrary.Models
{
    public class Soort
    {
        public enum TypeSoorten
        {
            NoType,
            Grass,
            Fire,
            Water
        }

        public TypeSoorten Naam { get; set; }
        public string ImageNaam { get; set; }

        public Soort(TypeSoorten naam, string imageNaam)
        {
            this.Naam = naam;
            this.ImageNaam = imageNaam;
        }

        public Soort()
        {

        }
    }
}
