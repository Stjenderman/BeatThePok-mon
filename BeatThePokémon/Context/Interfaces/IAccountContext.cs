﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelLibrary.Models;

namespace BeatThePokemon.Context.Interfaces
{
    public interface IAccountContext
    {
        bool Create(Account account);
        Account GetById(int id);
        Account GetByNaam(string naam);
        List<Pokemon> GetAllPokemonOfUser(int id);
        bool DeleteAllPokemonOfUser(int gebruikerId);
        //Account GetById(int id);
        //List<Account> GetAll();
        //bool Update(Account account);
        //bool Delete(int id);
    }
}
