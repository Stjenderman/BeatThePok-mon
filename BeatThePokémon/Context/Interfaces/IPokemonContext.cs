using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelLibrary.Models;

namespace BeatThePokemon.Context.Interfaces
{
    public interface IPokemonContext
    {
        bool Create(Pokemon pokemon);
        Pokemon GetById(int id);
        List<Pokemon> GetAll();
        //Pokemon Update(Pokemon pokemon);
        bool Delete(int id);
    }
}
