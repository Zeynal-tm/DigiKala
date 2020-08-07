using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using DigiKala.Core.Interfaces;
using DigiKala.Core.Services;
using DigiKala.Core.Classes;
using DigiKala.DataAccessLayer.Entities;
using DigiKala.Core.ViewModels;
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

        public IActionResult Activate()
        {
            ViewBag.IsOK = false;
            return View();
        }

        [HttpPost]
        public IActionResult Activate(StoreActivateViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                string username = User.Identity.Name;
                Store store = _user.GetUserStore(username);

                if (_user.ExistsMobileActivate(username, viewModel.MobileCode))
                {
                    if (_user.ExistsMailActivate(username, viewModel.MailCode))
                    {
                        _user.ActiveMobileNumber(username);
                        _user.ActiveMailAddress(store.Mail);

                        ViewBag.IsOK = true;
                    }
                    else
                    {
                        ModelState.AddModelError("MailCode", "کد فعالسازی شما اشتباه است");
                        ViewBag.IsOK = false;
                    }

                }
                else
                {
                    ModelState.AddModelError("MobileCode", "کد فعالسازی شما اشتباه است");
                    ViewBag.IsOK = false;
                }
            }
            return View(viewModel);
        }
    }
}
