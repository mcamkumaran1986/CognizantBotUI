using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AssignBot.Web.Controllers
{
    public class AssignBotController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BotAssignment()
        {
            return View();
        }
    }
}