using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeatThePokemon.Context.Interfaces;
using ModelLibrary.Models;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace BeatThePokemon.Repos
{
    public class AccountRepo
    {
        private IAccountContext ctx;
        private IAanvalContext anCtx;

        public AccountRepo(IAccountContext iaccountContext, IAanvalContext anctx)
        {
            this.ctx = iaccountContext;
            this.anCtx = anctx;
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
            Account a = ctx.GetById(accId);
            a.Pokemon = GetAllPokemonOfUser(a.Id);
            return a;
        }

        public List<Pokemon> GetAllPokemonOfUser(int gebruikerId)
        {
            List < Pokemon > pokemonList = ctx.GetAllPokemonOfUser(gebruikerId);
            foreach (Pokemon p in pokemonList)
            {
                p.Aanvallen = anCtx.GetAllByPokemon(p.Id);
            }
            return pokemonList;
        }
    }
}