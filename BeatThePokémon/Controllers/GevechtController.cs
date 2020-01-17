using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelLibrary.Models;
using BeatThePokemon.Models.Convert;
using BeatThePokemon.Models.ViewModels;
using BeatThePokemon.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeatThePokémon.Controllers
{
    public class GevechtController : Controller
    {
        private AccountRepo accountRepo;
        private GevechtRepo gevechtRepo;
        private AanvalRepo aanvalRepo;

        public GevechtController(AccountRepo acrepo, GevechtRepo grepo, AanvalRepo anrepo)
        {
            this.accountRepo = acrepo;
            this.gevechtRepo = grepo;
            this.aanvalRepo = anrepo;
        }

        [HttpGet]
        public IActionResult Index(int aanvalId)
        {
            if (HttpContext.Session.GetInt32("AccountID") == null) { return RedirectToAction("Login", "Account"); }

            if (!accountRepo.HasPokemon((int)HttpContext.Session.GetInt32("AccountID")))
            {
                return RedirectToAction("NewGame", "Home");
            }

            int userId = (int)HttpContext.Session.GetInt32("AccountID");

            if (!gevechtRepo.GevechtExists(userId))
            {
                gevechtRepo.CreateGevecht(accountRepo.GetUserById(userId));
            }

            int tegenstanderId = gevechtRepo.GetTegenstanderIdWithUserId(userId);
            Account a = accountRepo.GetUserById(userId);

            GevechtViewModel gvm = new GevechtViewModel();

            if (aanvalId != 0)
            {
                gevechtRepo.VoerGebruikerAanvalUit(aanvalId, a);
                gevechtRepo.VoerTegenstanderAanvalUit(a);
            }
                        
            if (gevechtRepo.CheckIfAllPokemonFainted(gevechtRepo.GetAllPokemonOfTegenstander(tegenstanderId, userId)))
            {
                gevechtRepo.StartNewGevecht(userId);
                gevechtRepo.CreateGevecht(accountRepo.GetUserById(userId));
            }            
            else if (gevechtRepo.CheckIfAllPokemonFainted(accountRepo.GetAllPokemonOfUser(userId)))
            {
                gevechtRepo.StartNewGame(userId);
                return RedirectToAction("Index", "Home");
            }

            Pokemon GebruikerPokemon = gevechtRepo.GetGebruikerPokemonByTeamId(gevechtRepo.GetGebruikerTeamId(userId));
            Pokemon TegenstanderPokemon = gevechtRepo.GetTegenstanderPokemonByTeamId(gevechtRepo.GetTegenstanderTeamId(userId));
            GevechtConvert agc = new GevechtConvert();
            gvm = agc.AccountToGevecht(GebruikerPokemon, TegenstanderPokemon);

            return View(gvm);
        }

        [HttpGet]
        public IActionResult Aanvallen(int pokId)
        {
            if (HttpContext.Session.GetInt32("AccountID") == null) { return RedirectToAction("Login", "Account"); }

            AanvallenGevechtViewModel agvm = new AanvallenGevechtViewModel();
            agvm.Aanvallen = AanvalViewModelConvert.AanvalListToViewModelList(aanvalRepo.GetAllByPokemon(pokId));

            return View(agvm);
        }

        [HttpPost]
        public IActionResult VoerAanvalUit(int aanvalId)
        {
            /*int userId = (int)HttpContext.Session.GetInt32("AccountID");
            int tegenstanderId = gevechtRepo.GetTegenstanderIdWithUserId(userId);

            Account a = accountRepo.GetUserById(userId);
            a.Tegenstander = gevechtRepo.GetById(tegenstanderId, userId);
            gevechtRepo.VoerGebruikerAanvalUit(aanvalId, a);
            gevechtRepo.VoerTegenstanderAanvalUit(a);*/

            return View("Index", aanvalId);
        }
    }
}