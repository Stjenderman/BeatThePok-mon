using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelLibrary.Models
{
    public enum HpValues
    {
        Low = 90,
        Normal = 100,
        High = 110
    }

    public class Pokemon
    {
        public int Id { get; }
        public int TeamId { get; set; }
        public string Naam { get; set; }
        public Soort Type { get; set; }
        public HpValues MaxHP { get; set; }
        public int HP { get; set; }
        public byte[] Uiterlijk { get; set; }
        public List<Aanval> Aanvallen { get; set; }
        public string Image { get; set; }

        public Pokemon(string naam, Soort type, List<Aanval> aanvallen, byte[] image)
        {
            this.Aanvallen = new List<Aanval>();

            this.Aanvallen = aanvallen;
            this.Naam = naam;
            this.Type = type;
            this.Uiterlijk = image;
        }

        public Pokemon(string naam, Soort type, List<Aanval> aanvallen, HpValues maxhp, int hp, byte[] image)
        {
            this.Aanvallen = new List<Aanval>();

            this.Aanvallen = aanvallen;
            this.Naam = naam;
            this.Type = type;
            this.MaxHP = maxhp;
            this.HP = hp;
            this.Uiterlijk = image;
        }

        public Pokemon(int id, string naam, Soort type, List<Aanval> aanvallen, byte[] image)
        {
            this.Aanvallen = new List<Aanval>();

            this.Aanvallen = aanvallen;
            this.Id = id;
            this.Naam = naam;
            this.Type = type;
            this.Uiterlijk = image;
        }

        public Pokemon(int id, int teamId, string naam, Soort type, List<Aanval> aanvallen, HpValues maxhp, int hp, byte[] image)
        {
            this.Aanvallen = new List<Aanval>();

            this.Aanvallen = aanvallen;
            this.Id = id;
            this.TeamId = teamId;
            this.Naam = naam;
            this.Type = type;
            this.MaxHP = maxhp;
            this.HP = hp;
            this.Uiterlijk = image;
        }

        public Pokemon()
        {
            Aanvallen = new List<Aanval>();
        }
    }
}
