using BeatThePokemon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Context.Interfaces
{
    public interface IGevechtContext
    {
        Tegenstander GetById(int id);
        List<int> GetAllId();
        bool LinkTegenstanderAanGevecht(int tegenstanderId, int accountId, List<Pokemon> pokemon);
        int GetTegenstanderIdWithUserId(int userId);
        bool CreateGevecht(Account account, Tegenstander tegenstander);
        int GetTegenstanderPokemonHpByTeamId(int teamId);
        int GetTegenstanderTeamId(int gebruikerId);
        bool UpdateHpTegenstander(int nieuwHp, int teamId);
        bool UpdateHpGebruiker(int nieuwHp, int teamId);
        bool GevechtExists(int gebruikerId);
        int GetGebruikerTeamId(int gebruikerId);
        Pokemon GetGebruikerPokemonByTeamId(int teamId);
        Pokemon GetTegenstanderPokemonByTeamId(int teamId);
    }
}
