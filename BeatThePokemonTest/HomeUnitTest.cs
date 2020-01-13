using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BeatThePokemon.Repos;
using BeatThePokemon.Context.Test;
using BeatThePokemon.Context.Interfaces;

namespace BeatThePokemonTest
{
    [TestClass]
    public class HomeUnitTest
    {
        //Test of je 3 verschillende cijfers krijgt
        [TestMethod]
        public void TestDrieRandNum()
        {
            //arrange
            List<int> mogelijkeNummers = new List<int>();

            int aantlNummersAlsResultaat = 4;

            for (int i = 23; i <= 26; i++)
            {
                mogelijkeNummers.Add(i);
            }

            IHomeContext homectx = new HomeTestContext();
            IPokemonContext pokectx = new PokemonTestContext();

            HomeRepo hr = new HomeRepo(homectx, pokectx);

            //act
            List<int> resultaat = hr.XRandNum(mogelijkeNummers, aantlNummersAlsResultaat);

            //assert
            for (int i = 0; i < resultaat.Count; i++)
            {
                for (int j = 0; j < resultaat.Count; j++)
                {
                    if(i != j)
                    {
                        Assert.AreNotEqual(resultaat[i], resultaat[j]);
                    }
                }
            }
        }

        //test of je een random getal krijgt
        [TestMethod]
        public void TestReturnHp()
        {
            //arrange
            IHomeContext homectx = new HomeTestContext();
            IPokemonContext pokectx = new PokemonTestContext();
            HomeRepo hr = new HomeRepo(homectx, pokectx);

            //act
            int hp = hr.ReturnHp();

            //assert
            Assert.AreNotEqual(-1, hp);
        }
    }
}
