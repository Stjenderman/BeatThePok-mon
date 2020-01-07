using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Models
{
    public class Aanval
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public int MaxPP { get; set; }
        public int PP { get; set; }
        public int Accuratie { get; set; }
        public int Power { get; set; }
        public Soort Soort { get; set; }

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
