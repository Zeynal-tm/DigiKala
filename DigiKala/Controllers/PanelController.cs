using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using DigiKala.Core.Interfaces;
using DigiKala.Core.Services;
using DigiKala.Core.Classes;
using DigiKala.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;

namespace DigiKala.Controllers
{
    [Authorize]
    public class PanelController : Controller
    {

        private IUser _user;

        public PanelController(IUser user)
        {
            _user = user;
        }


        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
