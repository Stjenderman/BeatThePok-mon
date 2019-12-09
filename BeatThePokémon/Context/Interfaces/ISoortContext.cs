using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeatThePokemon.Models;

namespace BeatThePokemon.Context.Interfaces
{
    public interface ISoortContext
    {
        //bool Create(Soort soort);
        //Soort GetById(int id);
        List<Soort> GetAll();
        //Soort Update(Soort soort);
        //bool Delete(int id);
    }
}
