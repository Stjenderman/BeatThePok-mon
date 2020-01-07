using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public GevechtController(AccountRepo arepo)
        {
            accountRepo = arepo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("AccountID") == null) { return RedirectToAction("Login", "Account"); }

            if (!accountRepo.HasPokemon((int)HttpContext.Session.GetInt32("AccountID")))
            {
                return RedirectToAction("NewGame", "Home");
            }
            AccountGevechtConvert agc = new AccountGevechtConvert();
            GevechtViewModel gvm = agc.AccountToGevecht(accountRepo.GetUserById((int)HttpContext.Session.GetInt32("AccountID")));

            return View(gvm);
        }
         
        [HttpGet]
        public IActionResult Aanvallen()
        {
            if (HttpContext.Session.GetInt32("AccountID") == null) { return RedirectToAction("Login", "Account"); }

            return View();
        }
    }
}