using BeatThePokemon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Context.Interfaces
{
    public interface IAanvalContext
    {
        bool Create(Aanval aanval);
        List<Aanval> GetAll();
        Aanval GetByName(string naam);
        List<Aanval> GetAllByPokemon(int id);
        Aanval GetById(int id);
    }
}
