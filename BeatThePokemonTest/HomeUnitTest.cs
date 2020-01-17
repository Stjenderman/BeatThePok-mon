using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BeatThePokemon.Repos;
using BeatThePokemon.Context.Test;
using BeatThePokemon.Context.Interfaces;
using ModelLibrary.Models;

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
            IAanvalContext aanvalctx = new AanvalTestContext();

            HomeRepo hr = new HomeRepo(homectx, pokectx, aanvalctx);

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
            IAanvalContext aanvalctx = new AanvalTestContext();

            HomeRepo hr = new HomeRepo(homectx, pokectx, aanvalctx);

            //act
            int hp = hr.ReturnHp();

            //assert
            Assert.AreNotEqual(-1, hp);
        }

        [TestMethod]
        public void TestGetPokemonWithIds()
        {
            //arrange
            IHomeContext hc = new HomeTestContext();
            IPokemonContext pc = new PokemonTestContext();
            IAanvalContext ac = new AanvalTestContext();
            HomeRepo hr = new HomeRepo(hc, pc, ac);

            List<Pokemon> p = new List<Pokemon>();
            List<int> ints = new List<int>();
            ints.Add(50);
            ints.Add(75);
            ints.Add(69);

            //act
            p = hr.GetPokemonWithIds(ints);

            //assert
            foreach (var pokemon in p)
            {
                Assert.IsNotNull(pokemon.Id);
                Assert.AreNotEqual(0, pokemon.Aanvallen.Count);
            }
        }
    }
}
