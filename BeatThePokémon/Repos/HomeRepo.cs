using BeatThePokemon.Context.Interfaces;
using ModelLibrary.Models;
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
        private IAanvalContext anCtx;

        public HomeRepo(IHomeContext hContext, IPokemonContext pContext, IAanvalContext anContext)
        {
            this.hCtx = hContext;
            this.pCtx = pContext;
            this.anCtx = anContext;
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
                Pokemon p = pCtx.GetById(i);
                p.Aanvallen = anCtx.GetAllByPokemon(p.Id);
                pokemon.Add(p);
            }
            return pokemon;
        }
    }
}
