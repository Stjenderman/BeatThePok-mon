using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Models
{


    public class Pokemon
    {
        public int Id { get; }
        public string Naam { get; set; }
        public Soort Type { get; set; }
        public byte[] Uiterlijk { get; set; }

        public Pokemon(string naam, Soort type, byte[] image)
        {
            this.Naam = naam;
            this.Type = type;
            this.Uiterlijk = image;
        }

        public Pokemon(int id, string naam, Soort type, byte[] image)
        {
            this.Id = id;
            this.Naam = naam;
            this.Type = type;
            this.Uiterlijk = image;
        }
    }
}
