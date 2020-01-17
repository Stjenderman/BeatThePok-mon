using BeatThePokemon.Context.Interfaces;
using ModelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Context.Test
{
    public class PokemonTestContext : IPokemonContext
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Pokemon> GetAll()
        {
            throw new NotImplementedException();
        }

        public Pokemon GetById(int id)
        {
            if(id != 0)
            {
                return new Pokemon(id, "Test", new Soort(Soort.TypeSoorten.Grass, "GrassIcon.png"), null, null);
            }
            return new Pokemon();
        }

        public bool LinkAanvalToPokemon(int pokId, int aanId, int maxPP)
        {
            throw new NotImplementedException();
        }

        public int Create(Pokemon pokemon)
        {
            throw new NotImplementedException();
        }
    }
}
