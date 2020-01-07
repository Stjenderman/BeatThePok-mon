using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeatThePokemon.Context.Interfaces;
using BeatThePokemon.Models;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace BeatThePokemon.Repos
{
    public class AccountRepo
    {
        private IAccountContext ctx;
        public AccountRepo(IAccountContext iaccountContext)
        {
            this.ctx = iaccountContext;
        }

        public bool Create(Account account)
        {
            return ctx.Create(account);
        }

        public bool Check(string gebruikersnaam, string wachtwoord)
        {
            Account a = ctx.GetByNaam(gebruikersnaam);
            return wachtwoord == a.Wachtwoord;
        }

        public int GetIdByName(string naam)
        {
            Account a = ctx.GetByNaam(naam);
            return a.Id;
        }

        public bool HasPokemon(int id)
        {
            if(ctx.GetAllPokemonOfUser(id).Count == 0)
            {
                return false;
            }
            return true;
        }

        public Account GetUserById(int accId)
        {
            return ctx.GetById(accId);
        }

        //public Account GetById()
        //public List<Account> GetAll()
        //public bool Update()
        //public bool Delete()
    }
}