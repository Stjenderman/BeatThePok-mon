using BeatThePokemon.Context.Interfaces;
using BeatThePokemon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Repos
{
    public class HomeRepo : BaseRepo
    {
        private IHomeContext hCtx;
        private IPokemonContext pCtx;

        public HomeRepo(IHomeContext hContext, IPokemonContext pContext)
        {
            this.hCtx = hContext;
            this.pCtx = pContext;
        }

        public List<int> GetAllId()
        {
            return hCtx.GetAllId();
        }
        
        public bool LinkPokemonToAccount(int pokeId, int accId)
        {
            return hCtx.LinkPokemonToAccount(pokeId, accId, ReturnHp());
        }

        public List<Pokemon> GetPokemonWithIds(List<int> ids)
        {
            List<Pokemon> pokemon = new List<Pokemon>();
            foreach (int i in ids)
            {
                pokemon.Add(pCtx.GetById(i));
            }
            return pokemon;
        }
    }
}
