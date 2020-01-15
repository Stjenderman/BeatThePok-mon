using BeatThePokemon.Context.Interfaces;
using ModelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Context.Test
{
    public class AanvalTestContext : IAanvalContext
    {
        public bool Create(Aanval aanval)
        {
            throw new NotImplementedException();
        }

        public List<Aanval> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Aanval> GetAllByPokemon(int id)
        {
            throw new NotImplementedException();
        }

        public Aanval GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Aanval GetByName(string naam)
        {
            throw new NotImplementedException();
        }
    }
}
