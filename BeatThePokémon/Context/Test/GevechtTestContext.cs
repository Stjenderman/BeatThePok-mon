using BeatThePokemon.Context.Interfaces;
using ModelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Context.Test
{
    public class GevechtTestContext : IGevechtContext
    {
        public bool CreateGevecht(Account account, Tegenstander tegenstander)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAllNewGameData(int gebruikerId)
        {
            throw new NotImplementedException();
        }

        public List<int> GetAllId()
        {
            throw new NotImplementedException();
        }

        public List<Pokemon> GetAllPokemonOfTegenstander(int tegenstanderId, int gebruikerId)
        {
            throw new NotImplementedException();
        }

        public Tegenstander GetById(int id, int gebruikerId)
        {
            throw new NotImplementedException();
        }

        public Pokemon GetGebruikerPokemonByTeamId(int teamId)
        {
            throw new NotImplementedException();
        }

        public int GetGebruikerTeamId(int gebruikerId)
        {
            throw new NotImplementedException();
        }

        public int GetTegenstanderIdWithUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public Pokemon GetTegenstanderPokemonByTeamId(int teamId)
        {
            throw new NotImplementedException();
        }

        public int GetTegenstanderPokemonHpByTeamId(int teamId)
        {
            throw new NotImplementedException();
        }

        public int GetTegenstanderTeamId(int gebruikerId)
        {
            throw new NotImplementedException();
        }

        public bool GevechtExists(int gebruikerId)
        {
            throw new NotImplementedException();
        }

        public bool LinkTegenstanderAanGevecht(int tegenstanderId, int accountId, List<Pokemon> pokemon)
        {
            throw new NotImplementedException();
        }

        public bool RestoreAllHpOfGebruiker(List<Pokemon> gebruikerPokemon, int gebruikerId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateHpGebruiker(int nieuwHp, int teamId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateHpTegenstander(int nieuwHp, int teamId)
        {
            throw new NotImplementedException();
        }
    }
}
