using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BeatThePokémon.Models;
using Microsoft.AspNetCore.Http;
using BeatThePokemon.Repos;
using BeatThePokemon.Models.Convert;
using BeatThePokemon.Models.ViewModels;
using BeatThePokemon.Models;

namespace BeatThePokémon.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private HomeRepo homeRepo;
        private PokemonRepo pokemonRepo;

        public HomeController(ILogger<HomeController> logger, HomeRepo hrepo, PokemonRepo prepo)
        {
            _logger = logger;
            this.homeRepo = hrepo;
            this.pokemonRepo = prepo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("AccountID") == null) { return RedirectToAction("Login", "Account"); }

            return View();
        }

        [HttpGet]
        public IActionResult NewGame()
        {
            if (HttpContext.Session.GetInt32("AccountID") == null) { return RedirectToAction("Login", "Account"); }

            PokemonNewGameConvert pngc = new PokemonNewGameConvert();
            List<int> allIds = homeRepo.GetAllId();
            List<int> randomInts = homeRepo.XRandNum(allIds, 3);
            List<Pokemon> randomPok = homeRepo.GetPokemonWithIds(randomInts);
            NewGameViewModel ngvm = pngc.PokemonToNewGame(randomPok);
            return View(ngvm);
        }

        [HttpPost]
        public IActionResult NewGame(NewGameViewModel ngvm)
        {
            Pokemon p = pokemonRepo.GetById(ngvm.PokemonId);
            if (homeRepo.LinkPokemonToAccount(ngvm.PokemonId, (int)HttpContext.Session.GetInt32("AccountID")))
            {
                return RedirectToAction("Index", "Gevecht");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            if (HttpContext.Session.GetInt32("AccountID") == null) { return RedirectToAction("Login", "Account"); }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
