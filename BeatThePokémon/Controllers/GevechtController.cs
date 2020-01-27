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
        public IActionResult Index(GevechtViewModel gvm)
        {
            if (HttpContext.Session.GetInt32("AccountID") == null) { return RedirectToAction("Login", "Account"); }

            int userId = (int)HttpContext.Session.GetInt32("AccountID");            

            if (!accountRepo.HasPokemon(userId))
            {
                return RedirectToAction("NewGame", "Home");
            }

            if (!gevechtRepo.GevechtExists(userId))
            {
                gevechtRepo.CreateGevecht(accountRepo.GetUserById(userId));
            }

            Pokemon GebruikerPokemon = gevechtRepo.GetGebruikerPokemonByTeamId(gevechtRepo.GetGebruikerTeamId(userId));
            Pokemon TegenstanderPokemon = gevechtRepo.GetTegenstanderPokemonByTeamId(gevechtRepo.GetTegenstanderTeamId(userId));
            gvm.GebruikerPokemon = PokemonViewModelConvert.PokemonToPokemonViewModel(GebruikerPokemon);
            gvm.TegenstanderPokemon = PokemonViewModelConvert.PokemonToPokemonViewModel(TegenstanderPokemon);

            gvm = GevechtConvert.ImageConvert(gvm);

            return View("Index", gvm);
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
        public IActionResult VoerAanvalUit(int gAanvalId)
        {
            Account a = accountRepo.GetUserById((int)HttpContext.Session.GetInt32("AccountID"));
            int tegenstanderTeamId = gevechtRepo.GetTegenstanderTeamId(a.Id);
            int gebruikerTeamId = gevechtRepo.GetGebruikerTeamId(a.Id);

            int gOudeHp = gevechtRepo.GetGebruikerPokemonByTeamId(gebruikerTeamId).HP;
            int tOudeHp = gevechtRepo.GetTegenstanderPokemonByTeamId(tegenstanderTeamId).HP;

            Aanval gAanval = gevechtRepo.VoerGebruikerAanvalUit(gAanvalId, tegenstanderTeamId, tOudeHp);
            Aanval tAanval = gevechtRepo.VoerTegenstanderAanvalUit(gebruikerTeamId, gOudeHp, tegenstanderTeamId);

            GevechtViewModel gvm = new GevechtViewModel();
            gvm.GebruikerAanval = AanvalViewModelConvert.AanvalToViewModel(gAanval);
            gvm.TegenstanderAanval = AanvalViewModelConvert.AanvalToViewModel(tAanval);
            gvm.OudGebruikerHp = gOudeHp;
            gvm.OudtegenstanderHp = tOudeHp;

            return Index(gvm);
        }

        [HttpPost]
        public IActionResult NewGevecht()
        {
            Account a = accountRepo.GetUserById((int)HttpContext.Session.GetInt32("AccountID"));
            gevechtRepo.StartNewGevecht(a.Id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult NewGame()
        {
            Account a = accountRepo.GetUserById((int)HttpContext.Session.GetInt32("AccountID"));
            gevechtRepo.StartNewGame(a.Id);

            return RedirectToAction("Index");
        }
    }
}