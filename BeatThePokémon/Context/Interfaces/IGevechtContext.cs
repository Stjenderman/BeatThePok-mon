using ModelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Context.Interfaces
{
    public interface IGevechtContext
    {
        Tegenstander GetById(int id, int gebruikerId);
        List<int> GetAllId();
        bool LinkTegenstanderAanGevecht(int tegenstanderId, int accountId, List<Pokemon> pokemon);
        int GetTegenstanderIdWithUserId(int userId);
        bool CreateGevecht(Account account, Tegenstander tegenstander);
        int GetTegenstanderTeamId(int gebruikerId);
        List<Pokemon> GetAllPokemonOfTegenstander(int tegenstanderId, int gebruikerId);
        bool UpdateHpTegenstander(int nieuwHp, int teamId);
        bool UpdateHpGebruiker(int nieuwHp, int teamId);
        bool GevechtExists(int gebruikerId);
        int GetGebruikerTeamId(int gebruikerId);
        Pokemon GetGebruikerPokemonByTeamId(int teamId);
        Pokemon GetTegenstanderPokemonByTeamId(int teamId);
        bool DeleteAllNewGameData(int gebruikerId);
        bool RestoreAllHpOfGebruiker(List<Pokemon> gebruikerPokemon, int gebruikerId);
    }
}
