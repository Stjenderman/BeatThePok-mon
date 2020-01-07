using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeatThePokemon.Models.Convert;
using BeatThePokemon.Models.ViewModels;
using BeatThePokemon.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeatThePokemon.Controllers
{
    public class AccountController : Controller
    {
        private AccountRepo accountRepo;
        public AccountController(AccountRepo arepo)
        {
            this.accountRepo = arepo;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                if (accountRepo.Check(lvm.Gebruikersnaam, lvm.Wachtwoord))
                {
                    HttpContext.Session.SetInt32("AccountID", accountRepo.GetIdByName(lvm.Gebruikersnaam));
                    HttpContext.Session.SetString("Gebruikernaam", lvm.Gebruikersnaam);
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(lvm);
        }

        [HttpGet]
        public IActionResult Registratie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registratie(RegistratieViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                AccountRegistratieConvert arc = new AccountRegistratieConvert();
                accountRepo.Create(arc.RegistratieToAccount(rvm));
                return RedirectToAction("Login");
            }
            return View(rvm);
        }
    }
}