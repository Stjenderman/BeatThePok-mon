using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeatThePokemon.Context.Interfaces;
using BeatThePokemon.Models;

namespace BeatThePokemon.Repos
{
    public class PokemonRepo
    {
        private readonly IPokemonContext ctx;
        public PokemonRepo(IPokemonContext context)
        {
            this.ctx = context;
        }

        public bool Create(Pokemon p)
        {
            if (p.Type.Naam == 0)
            { return false; }
            if (ctx.Create(p))
            { return true; }

            return false;
        }

        public List<Pokemon> GetAll()
        {
            return ctx.GetAll();
        }

        public bool Delete(int id)
        {
            if (ctx.Delete(id))
            { return true; }

            else
            { return false; }
        }
    }
}
