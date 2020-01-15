using BeatThePokemon.Context.Interfaces;
using ModelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatThePokemon.Repos
{
    public class BaseRepo
    {
        public List<int> XRandNum(List<int> mogelijkeNummers, int aantalRandomNummers)
        {
            List<int> drieRandNum = new List<int>();
            Random random = new Random(Guid.NewGuid().GetHashCode());

            while (drieRandNum.Count < aantalRandomNummers)
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

        public int ReturnHp()
        {
            Random rand = new Random();
            int randNum = rand.Next(1, 7);
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
