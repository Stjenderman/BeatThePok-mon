using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelLibrary.Models
{
    public class Aanval
    {
        public int Id { get; }
        public string Naam { get; }
        public int MaxPP { get; }
        public int PP { get; }
        public int Accuratie { get; }
        public int Power { get; }
        public Soort Soort { get; }

        public Aanval(int id, string naam, int maxPP, int accuratie, int power, Soort soort)
        {
            this.Id = id;
            this.Naam = naam;
            this.MaxPP = maxPP;
            this.PP = maxPP;
            this.Accuratie = accuratie;
            this.Power = power;
            this.Soort = soort;
        }

        public Aanval(int id, string naam, int maxPP, int PP, int accuratie, int power, Soort soort)
        {
            this.Id = id;
            this.Naam = naam;
            this.MaxPP = maxPP;
            this.PP = PP;
            this.Accuratie = accuratie;
            this.Power = power;
            this.Soort = soort;
        }

        public Aanval()
        {

        }
    }
}
