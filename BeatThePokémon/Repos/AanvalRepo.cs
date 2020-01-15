using BeatThePokemon.Context.Interfaces;
using ModelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Repos
{
    public class AanvalRepo
    {
        private IAanvalContext ctx;
        public AanvalRepo(IAanvalContext aanvalContext)
        {
            this.ctx = aanvalContext;
        }

        public bool Create(Aanval aanval)
        {
            return ctx.Create(aanval);
        }

        public List<Aanval> GetAll()
        {
            return ctx.GetAll();
        }

        public Aanval GetByName(string naam)
        {
            return ctx.GetByName(naam);
        }

        public List<Aanval> GetAllByPokemon(int pokId)
        {
            return ctx.GetAllByPokemon(pokId);
        }
    }
}
