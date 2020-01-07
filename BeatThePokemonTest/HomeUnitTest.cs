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
            for (int i = 23; i <= 26; i++)
            {
                mogelijkeNummers.Add(i);
            }

            IHomeContext homectx = new HomeTestContext();
            IPokemonContext pokectx = new PokemonTestContext();

            HomeRepo hr = new HomeRepo(homectx, pokectx);

            //act
            List<int> resultaat = hr.DrieRandNum(mogelijkeNummers);
            int int1 = resultaat[0];
            int int2 = resultaat[1];
            int int3 = resultaat[2];

            //assert
            Assert.AreNotEqual(int1, int2);
            Assert.AreNotEqual(int2, int3);
            Assert.AreNotEqual(int1, int3);
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
