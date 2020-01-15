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
        public bool Create(Pokemon pokemon)
        {
            throw new NotImplementedException();
        }

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
            throw new NotImplementedException();
        }
    }
}
