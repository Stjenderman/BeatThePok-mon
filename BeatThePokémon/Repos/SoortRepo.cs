using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeatThePokemon.Context.Interfaces;
using ModelLibrary.Models;

namespace BeatThePokemon.Repos
{
    public class SoortRepo
    {
        private readonly ISoortContext ctx;

        public SoortRepo(ISoortContext context)
        {
            this.ctx = context;
        }

        public List<Soort> GetAll()
        {
            return ctx.GetAll();
        }
    }
}
