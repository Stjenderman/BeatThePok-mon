using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeatThePokemon.Context.MSSQL;
using ModelLibrary.Models;
using BeatThePokemon.Models.Convert;
using BeatThePokemon.Models.ViewModels;
using BeatThePokemon.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeatThePokemon.Controllers
{
    public class PokemonController : Controller
    {
        private PokemonRepo pokemonRepo;
        private SoortRepo soortRepo;
        private AanvalRepo aanvalRepo;

        public PokemonController(PokemonRepo pRepo, SoortRepo sRepo, AanvalRepo aRepo)
        {
            this.pokemonRepo = pRepo;
            this.soortRepo = sRepo;
            this.aanvalRepo = aRepo;
        }

        [HttpGet]
        public IActionResult Create()
        {
            pokemonRepo.GetById(29);

            return View();
        }

        [HttpGet]
        public IActionResult CreatePokemon()
        {
            if (HttpContext.Session.GetInt32("AccountID") == null) { return RedirectToAction("Login", "Account"); }

            PokemonCreateViewModel pcvm = new PokemonCreateViewModel();
            pcvm.AlleSoorten = soortRepo.GetAll();
            return View(pcvm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePokemon(PokemonCreateViewModel pcvm)
        {
            if (ModelState.IsValid)
            {
                PokemonCreateConvert pcc = new PokemonCreateConvert();
                pcvm.ToeTeVoegenAanval = aanvalRepo.GetByName(pcvm.NaamAanval);
                Pokemon p = pcc.CreateToPokemon(pcvm);
                pokemonRepo.Create(p);
                return RedirectToAction("AllePokemon");
            }
            pcvm.AlleSoorten = soortRepo.GetAll();
            return View(pcvm);
        }

        [HttpGet]
        public IActionResult CreateAanval()
        {
            if (HttpContext.Session.GetInt32("AccountID") == null) { return RedirectToAction("Login", "Account"); }

            AanvalCreateViewModel acvm = new AanvalCreateViewModel();
            acvm.AlleSoorten = soortRepo.GetAll();
            return View(acvm);
        }

        [HttpPost]
        public IActionResult CreateAanval(AanvalCreateViewModel acvm)
        {
            AanvalCreateConvert acc = new AanvalCreateConvert();
            aanvalRepo.Create(acc.CreateToAanval(acvm));
            acvm.AlleSoorten = soortRepo.GetAll();
            return View(acvm);
        }

        [HttpGet]
        public IActionResult AllePokemon()
        {
            if (HttpContext.Session.GetInt32("AccountID") == null) { return RedirectToAction("Login", "Account"); }

            AllePokemonConvert pvc = new AllePokemonConvert();
            AllePokemonViewModel apvm = pvc.PokemonToView(pokemonRepo.GetAll());
            return View(apvm);
        }

        [HttpPost]
        public IActionResult Delete(AllePokemonViewModel apvm)
        {
            pokemonRepo.Delete(apvm.Id);
            return RedirectToAction("AllePokemon");
        }

        public IActionResult Aanvallen(string term)
        {
            List<Aanval> aanvallen = aanvalRepo.GetAll();

            List<string> returnList = new List<string>();
            foreach (Aanval s in aanvallen)
            {
                if (s.Naam.StartsWith(term))
                {
                    returnList.Add(s.Naam);
                }
            }

            return Json(returnList);
        }
    }
}