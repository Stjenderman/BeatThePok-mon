using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeatThePokemon.Context.Interfaces;
using ModelLibrary.Models;

namespace BeatThePokemon.Repos
{
    public class PokemonRepo
    {
        private readonly IPokemonContext ctx;
        private IAanvalContext anCtx;

        public PokemonRepo(IPokemonContext context, IAanvalContext anContext)
        {
            this.ctx = context;
            this.anCtx = anContext;
        }

        public bool Create(Pokemon p)
        {
            if (p.Type.Naam == 0)
            { return false; }

            int pokemonId = ctx.Create(p);
            foreach (Aanval aanval in p.Aanvallen)
            {
                if(!ctx.LinkAanvalToPokemon(pokemonId, aanval.Id, aanval.MaxPP))
                {
                    return false;
                }
            }

            return true;
        }

        public Pokemon GetById(int id)
        {
            Pokemon p = ctx.GetById(id);
            p.Aanvallen = anCtx.GetAllByPokemon(p.Id);
            return p;
        }

        public List<Pokemon> GetAll()
        {
            List<Pokemon> pokemonList = ctx.GetAll();
            foreach (Pokemon p in pokemonList)
            {
                p.Aanvallen = anCtx.GetAllByPokemon(p.Id);
            }
            return pokemonList;
        }

        public bool Delete(int id)
        {
            return ctx.Delete(id);
        }
    }
}
