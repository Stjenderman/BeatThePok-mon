using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Context.Interfaces
{
    public interface IHomeContext
    {
        List<int> GetAllId();
        bool LinkPokemonToAccount(int pokeId, int accId, int maxHP);
    }
}
