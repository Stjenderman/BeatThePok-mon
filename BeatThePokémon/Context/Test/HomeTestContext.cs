using BeatThePokemon.Context.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Context.Test
{
    public class HomeTestContext : IHomeContext
    {
        public List<int> GetAllId()
        {
            throw new NotImplementedException();
        }

        public bool LinkPokemonToAccount(int pokeId, int accId, int maxHP)
        {
            throw new NotImplementedException();
        }
    }
}
