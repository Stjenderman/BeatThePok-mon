using BeatThePokemon.Context.Interfaces;
using BeatThePokemon.Context.Test;
using ModelLibrary.Models;
using BeatThePokemon.Repos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeatThePokemonTest
{
    [TestClass]
    public class GevechtUnitTest
    {
        //Test of er de juist hp uit de methode komt
        [TestMethod]
        public void TestNewHp()
        {
            //arange
            IGevechtContext gctx = new GevechtTestContext();
            IPokemonContext pctx = new PokemonTestContext();
            IHomeContext hctx = new HomeTestContext();
            IAanvalContext anctx = new AanvalTestContext();
            IAccountContext acctx = new AccountTestContext();

            GevechtRepo gr = new GevechtRepo(gctx, pctx, hctx, anctx, acctx);

            int oudeHp = 100;
            Aanval aanval = new Aanval(1, "Water Pulse", 20, 20, 50, 80, new Soort(Soort.TypeSoorten.Water, "WaterIcon.png"));
            int randomGetal = 2;

            int nieuweHp = -1;
            int verwachtUitkomst = 20;

            //act
            nieuweHp = gr.NewHp(oudeHp, aanval, randomGetal);

            //assert
            Assert.AreEqual(verwachtUitkomst, nieuweHp);
        }
    }
}
