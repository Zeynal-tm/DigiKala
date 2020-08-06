using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using DigiKala.Core.Classes;

namespace DigiKala.Controllers
{
    public class HomeController : Controller
    {
        //[RoleAttribute(9)]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
