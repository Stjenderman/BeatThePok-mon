using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BeatThePokémon.Controllers
{
    public class GevechtController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Aanvallen()
        {
            return View();
        }
    }
}