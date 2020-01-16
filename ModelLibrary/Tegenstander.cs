using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelLibrary.Models
{
    public class Tegenstander
    {
        public int Id { get; }
        public string Naam { get; }
        public List<Pokemon> Pokemon { get; set; }

        public Tegenstander()
        {

        }

        public Tegenstander(string naam, List<Pokemon> pokemon)
        {
            this.Naam = naam;
            this.Pokemon = pokemon;
        }

        public Tegenstander(int id, string naam, List<Pokemon> pokemon)
        {
            this.Id = id;
            this.Naam = naam;
            this.Pokemon = pokemon;
        }
    }
}
