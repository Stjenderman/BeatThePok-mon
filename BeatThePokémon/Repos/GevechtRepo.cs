using BeatThePokemon.Context.Interfaces;
using BeatThePokemon.Models;
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
        private IAanvalContext aCtx;

        public GevechtRepo(IGevechtContext gContext, IPokemonContext pContext, IHomeContext hContext, IAanvalContext aContext)
        {
            this.gCtx = gContext;
            this.pCtx = pContext;
            this.hCtx = hContext;
            this.aCtx = aContext;
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

        public Tegenstander GetById(int id)
        {
            return gCtx.GetById(id);
        }

        public List<Pokemon> GetPokemonWithIds(List<int> ids)
        {
            List<Pokemon> pokemon = new List<Pokemon>();
            foreach (int i in ids)
            {
                pokemon.Add(pCtx.GetById(i));
            }
            return pokemon;
        }

        public bool VoerGebruikerAanvalUit(int aanvalId, Account a)
        {
            int tegenstanderTeamId = GetTegenstanderTeamId(a.Id);
            int hp = GetTegenstanderPokemonHpByTeamId(tegenstanderTeamId);
            Aanval aanval = aCtx.GetById(aanvalId);
            int nieuwHp = GetRandomNewHp(hp, aanval);

            return gCtx.UpdateHpTegenstander(nieuwHp, tegenstanderTeamId);
        }

        public bool VoerTegenstanderAanvalUit(Account a)
        {
            int gebruikerTeamId = GetGebruikerTeamId(a.Id);
            Pokemon tegenstanderPokemon = GetTegenstanderPokemonByTeamId(GetTegenstanderTeamId(a.Id));
            Aanval aanval = GetRandomAanval(tegenstanderPokemon);
            int oudeHp = GetGebruikerPokemonByTeamId(GetGebruikerTeamId(a.Id)).HP;
            int nieuweHp = GetRandomNewHp(oudeHp, aanval);

            return gCtx.UpdateHpGebruiker(nieuweHp, gebruikerTeamId);
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
            int nieuweHp = -1;
            
            if(randGetal <= a.Accuratie - 10)
            {
                nieuweHp = oudeHp - a.Power;
            }
            else if(randGetal >= 90)
            {
                nieuweHp = oudeHp - (a.Power * 2);
            }
            else
            {
                nieuweHp = oudeHp;
            }

            return nieuweHp;
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
            return gCtx.GetGebruikerPokemonByTeamId(teamId);
        }

        public Pokemon GetTegenstanderPokemonByTeamId(int teamId)
        {
            return gCtx.GetTegenstanderPokemonByTeamId(teamId);
        }

        public int GetTegenstanderPokemonHpByTeamId(int teamId)
        {
            return gCtx.GetTegenstanderPokemonHpByTeamId(teamId);
        }

        public bool CreateGevecht(Account account)
        {
            CreateTegenstander(account);
            int tegenstanderId = GetTegenstanderIdWithUserId(account.Id);
            return gCtx.CreateGevecht(account, GetById(tegenstanderId));
        }

        public bool GevechtExists(int gebruikerId)
        {
            return gCtx.GevechtExists(gebruikerId);
        }
    }
}
