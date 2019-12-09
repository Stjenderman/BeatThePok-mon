using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeatThePokemon.Models;

namespace BeatThePokemon.Context.Interfaces
{
    public interface IAccountContext
    {
        bool Create(Account account);
        Account GetByNaam(string naam);

        //Account GetById(int id);
        //List<Account> GetAll();
        //bool Update(Account account);
        //bool Delete(int id);
    }
}
