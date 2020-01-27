using BeatThePokemon.Context.Interfaces;
using ModelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Repos
{
    public class GevechtRepo : BaseRepo
    {
        private IGevechtContext gCtx;
        private IPokemonContext pCtx;
        private IHomeContext hCtx;
        private IAanvalContext anCtx;
        private IAccountContext acCtx;

        public GevechtRepo(IGevechtContext gContext, IPokemonContext pContext, IHomeContext hContext, IAanvalContext anContext, IAccountContext acContext)
        {
            this.gCtx = gContext;
            this.pCtx = pContext;
            this.hCtx = hContext;
            this.anCtx = anContext;
            this.acCtx = acContext;
        }

        public bool CreateTegenstander(Account account)
        {
            int tegenstanderId = XRandNum(GetAllTegenstanderId(), 1)[0];
            List<Pokemon> pokemon = GetPokemonWithIds(XRandNum(hCtx.GetAllId(), account.Pokemon.Count));
            foreach (Pokemon p in pokemon)
            {
                p.MaxHP = (HpValues)ReturnHp();
            }

            return gCtx.LinkTegenstanderAanGevecht(tegenstanderId, account.Id, pokemon);
        }

        public List<int> GetAllTegenstanderId()
        {
            return gCtx.GetAllId();
        }

        public int GetTegenstanderIdWithUserId(int userId)
        {
            return gCtx.GetTegenstanderIdWithUserId(userId);
        }

        public Tegenstander GetById(int id, int gebruikerId)
        {
            Tegenstander t = gCtx.GetById(id, gebruikerId);
            t.Pokemon = GetAllPokemonOfTegenstander(t.Id, gebruikerId);
            return t;
        }

        public List<Pokemon> GetPokemonWithIds(List<int> ids)
        {
            List<Pokemon> pokemon = new List<Pokemon>();
            foreach (int i in ids)
            {
                Pokemon p = pCtx.GetById(i);
                p.Aanvallen = anCtx.GetAllByPokemon(p.Id);
                pokemon.Add(p);
            }
            return pokemon;
        }

        public Aanval VoerGebruikerAanvalUit(int aanvalId, int tegenstanderTeamId, int hp)
        {
            Aanval aanval = anCtx.GetById(aanvalId);
            int nieuwHp = GetRandomNewHp(hp, aanval);
            gCtx.UpdateHpTegenstander(nieuwHp, tegenstanderTeamId);

            return aanval;
        }

        public Aanval VoerTegenstanderAanvalUit(int gebruikerTeamId, int oudeHp, int tegenstanderTeamId)
        {
            Pokemon tegenstanderPokemon = GetTegenstanderPokemonByTeamId(tegenstanderTeamId);
            Aanval aanval = GetRandomAanval(tegenstanderPokemon);
            int nieuweHp = GetRandomNewHp(oudeHp, aanval);
            gCtx.UpdateHpGebruiker(nieuweHp, gebruikerTeamId);

            return aanval;
        }

        public Aanval GetRandomAanval(Pokemon p)
        {
            Random rand = new Random();
            int randomAanval = rand.Next(0, p.Aanvallen.Count - 1);
            return p.Aanvallen[randomAanval];
        }

        public int GetRandomNewHp(int oudeHp, Aanval a)
        {
            Random rand = new Random();
            int randGetal = rand.Next(1, 101);
            return NewHp(oudeHp, a, randGetal);
        }

        public int NewHp(int oudeHp, Aanval a, int randGetal)
        {
            int nieuweHp;

            if (randGetal <= a.Accuratie - 10)
            {
                nieuweHp = oudeHp - a.Power;
            }
            else if (randGetal >= 90)
            {
                nieuweHp = oudeHp - a.Power * 20;
            }
            else
            {
                nieuweHp = oudeHp;
            }

            return nieuweHp;
        }

        public bool CheckIfAllPokemonFainted(List<Pokemon> pokemon)
        {
            foreach (var p in pokemon)
            {
                if (p.HP > 0)
                {
                    return false;
                }
            }
            return true;
        }

        public int GetTegenstanderTeamId(int gebruikerId)
        {
            return gCtx.GetTegenstanderTeamId(gebruikerId);
        }

        public int GetGebruikerTeamId(int gebruikerId)
        {
            return gCtx.GetGebruikerTeamId(gebruikerId);
        }

        public Pokemon GetGebruikerPokemonByTeamId(int teamId)
        {
            Pokemon p = gCtx.GetGebruikerPokemonByTeamId(teamId);
            p.Aanvallen = anCtx.GetAllByPokemon(p.Id);
            return p;
        }

        public Pokemon GetTegenstanderPokemonByTeamId(int teamId)
        {
            Pokemon p = gCtx.GetTegenstanderPokemonByTeamId(teamId);
            p.Aanvallen = anCtx.GetAllByPokemon(p.Id);
            return p;
        }

        public List<Pokemon> GetAllPokemonOfTegenstander(int tegenstanderId, int gebruikerId)
        {
            List<Pokemon> pList = new List<Pokemon>();
            foreach (Pokemon p in pList)
            {
                p.Aanvallen = anCtx.GetAllByPokemon(p.Id);
            }
            return gCtx.GetAllPokemonOfTegenstander(tegenstanderId, gebruikerId);
        }

        public bool CreateGevecht(Account account)
        {
            CreateTegenstander(account);
            int tegenstanderId = GetTegenstanderIdWithUserId(account.Id);
            return gCtx.CreateGevecht(account, GetById(tegenstanderId, account.Id));
        }

        public bool GevechtExists(int gebruikerId)
        {
            return gCtx.GevechtExists(gebruikerId);
        }

        public bool StartNewGevecht(int gebruikerId)
        {
            List<Pokemon> allePokemon = new List<Pokemon>();
            allePokemon = acCtx.GetAllPokemonOfUser(gebruikerId);
            return gCtx.DeleteAllNewGameData(gebruikerId) && gCtx.RestoreAllHpOfGebruiker(allePokemon, gebruikerId);
        }

        public bool StartNewGame(int gebruikerId)
        {
            return StartNewGevecht(gebruikerId) && acCtx.DeleteAllPokemonOfUser(gebruikerId);
        }
    }
}
