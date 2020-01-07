using BeatThePokemon.Context.Interfaces;
using BeatThePokemon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Repos
{
    public class HomeRepo
    {
        private IHomeContext hCtx;
        private IPokemonContext pCtx;

        public HomeRepo(IHomeContext hContext, IPokemonContext pContext)
        {
            this.hCtx = hContext;
            this.pCtx = pContext;
        }

        public List<int> DrieRandNum(List<int> mogelijkeNummers)
        {
            List<int> drieRandNum = new List<int>();
            Random random = new Random(Guid.NewGuid().GetHashCode());

            while (drieRandNum.Count < 3)
            {
                int randomPos = random.Next(0, mogelijkeNummers.Count);
                int nieuwInt = mogelijkeNummers[randomPos];
                if (!drieRandNum.Contains(nieuwInt))
                {
                    drieRandNum.Add(nieuwInt);
                }
            }

            return drieRandNum;
        }

        public List<int> GetAllId()
        {
            return hCtx.GetAllId();
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

        public bool LinkPokemonToAccount(int pokeId, int accId)
        {
            return hCtx.LinkPokemonToAccount(pokeId, accId, ReturnHp());
        }

        public int ReturnHp()
        {
            Random rand = new Random();
            int randNum = rand.Next(1,7);
            switch (randNum)
            {
                case 1:
                    {
                        return (int)HpValues.High;
                    }
                case 2:
                case 3:
                case 4:
                    {
                        return (int)HpValues.Normal;
                    }
                case 5:
                case 6:
                    {
                        return (int)HpValues.Low;
                    }
            }
            return -1;
        }
    }
}
