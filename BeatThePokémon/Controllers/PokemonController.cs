using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeatThePokemon.Context.MSSQL;
using BeatThePokemon.Models;
using BeatThePokemon.Models.Convert;
using BeatThePokemon.Models.ViewModels;
using BeatThePokemon.Repos;
using Microsoft.AspNetCore.Mvc;

namespace BeatThePokemon.Controllers
{
    public class PokemonController : Controller
    {
        private PokemonRepo pokemonRepo;
        private SoortRepo soortRepo;

        public PokemonController(PokemonRepo pRepo, SoortRepo sRepo)
        {
            this.pokemonRepo = pRepo;
            this.soortRepo = sRepo;
        }

        [HttpGet]
        public IActionResult Create()
        {
            PokemonCreateViewModel pcvm = new PokemonCreateViewModel();
            pcvm.AlleSoorten = soortRepo.GetAll();
            return View(pcvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PokemonCreateViewModel pcvm)
        {
            pcvm.AlleSoorten = soortRepo.GetAll();
            if (ModelState.IsValid)
            {
                PokemonCreateConvert pcc = new PokemonCreateConvert();
                pokemonRepo.Create(pcc.CreateToPokemon(pcvm));
                return RedirectToAction("AllePokemon");
            }
            else
            {
                return View(pcvm);
            }
        }

        public IActionResult AllePokemon()
        {
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
    }
}