using BeatThePokemon.Context.Interfaces;
using ModelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Context.Test
{
    public class AccountTestContext : IAccountContext
    {
        public bool Create(Account account)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAllPokemonOfUser(int gebruikerId)
        {
            throw new NotImplementedException();
        }

        public List<Pokemon> GetAllPokemonOfUser(int id)
        {
            throw new NotImplementedException();
        }

        public Account GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Account GetByNaam(string naam)
        {
            throw new NotImplementedException();
        }
    }
}
