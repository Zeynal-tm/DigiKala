using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using DigiKala.Core.Interfaces;
using DigiKala.Core.Services;
using DigiKala.Core.ViewModels;
using DigiKala.Core.Classes;

using DigiKala.DataAccessLayer.Entities;

using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DigiKala.Controllers
{
    public class AccountsController : Controller
    {
        private IAccount _account;
        private IViewRenderService _render;

        private PersianCalendar pc = new PersianCalendar();

        public AccountsController(IAccount account, IViewRenderService render)
        {
            _account = account;
            _render = render;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (_account.ExistsMobileNumber(viewModel.Mobile))
                {
                    //Go To Login
                }
                else
                {
                    User user = new User()
                    {
                        Mobile = viewModel.Mobile,
                        ActiveCode = CodeGenerators.ActiveCode(),
                        Code = null,
                        Date = pc.GetYear(DateTime.Now).ToString("0000") + "/" + pc.GetMonth(DateTime.Now).ToString("00") + "/"
                                + pc.GetDayOfMonth(DateTime.Now).ToString("00"),
                        Fullname = null,
                        IsActive = false,
                        Password = HashGenerators.MD5Encoding(viewModel.Password),
                        RoleId = _account.GetMaxRole()
                    };

                    _account.AddUser(user);

                    try
                    {
                        MessageSender sender = new MessageSender();

                        sender.SMS(viewModel.Mobile, "به فروشگاه اینترنتی خوش آمدید"
                            + Environment.NewLine + "کد فعال سازی :" + user.ActiveCode);
                    }
                    catch
                    {

                    }

                    return RedirectToAction(nameof(Activate));

                    //Go To Activite
                }
            }
            return View(viewModel);
        }

        public IActionResult Activate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Activate(ActivateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (_account.ActivateUser(viewModel.ActiveCode))
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    ModelState.AddModelError("ActiveCode", "کد فعال سازی شما معتبر نیست");
                }
            }

            return View(viewModel);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string hashPassword = HashGenerators.MD5Encoding(viewModel.Password);

                User user = _account.LoginUser(viewModel.Mobile, hashPassword);

                if (user != null)
                {
                    if (user.Role.Name == "فروشگاه")
                    {
                        var claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.Name, user.Mobile)
                        };

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);

                        var properties = new AuthenticationProperties()
                        {
                            IsPersistent = true
                        };

                        HttpContext.SignInAsync(principal, properties);

                        return RedirectToAction("Dashboard", "Panel");
                    }
                    else
                    {
                        if (user.IsActive)
                        {
                            var claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.Name, user.Mobile)
                        };

                            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var principal = new ClaimsPrincipal(identity);

                            var properties = new AuthenticationProperties()
                            {
                                IsPersistent = true
                            };

                            HttpContext.SignInAsync(principal, properties);

                            if (user.Role.Name == "کاربر")
                            {
                                return RedirectToAction("Dashboard", "Home");
                            }
                            else
                            {
                                return RedirectToAction("Dashboard", "Panel");
                            }
                        }
                        else
                        {
                            return RedirectToAction(nameof(Activate));
                        }
                    }
                }

                    
                else
                {
                    ModelState.AddModelError("Password", "مشخصات کاربری اشتباه است");
                }
            }
            {

            }
            return View(viewModel);
        }

        public IActionResult Forget()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Forget(ForgetViewModel viewModel)                  //قسمت هشتم
        {
            if (ModelState.IsValid)
            {
                if (_account.ExistsMobileNumber(viewModel.Mobile))
                {
                    try
                    {
                        MessageSender sender = new MessageSender();

                        sender.SMS(viewModel.Mobile, "امکان تغییر کلمه عبور با کد تایید"
                            + _account.GetUserActiveCode(viewModel.Mobile));
                    }
                    catch
                    {

                    }

                    return RedirectToAction(nameof(Reset));
                }
                else
                {
                    ModelState.AddModelError("Mobile", "کاربری با این شماره وجود ندارد");
                }
            }


            return View(viewModel);
        }

        public IActionResult Reset()                //قسمت هشتم
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Reset(ResetViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (_account.ResetPassword(viewModel.ActiveCode, viewModel.Password))
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    ModelState.AddModelError("ActiveCode", "کد تایید شما اشتباه است");
                }
            }
            return View(viewModel);
        }

        public IActionResult Store()
        {
            ViewBag.MyMessage = false;
            return View();
        }

        [HttpPost]
        public IActionResult Store(StoreRegisterViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                if (_account.ExistsMailAddress(viewModel.Mail))
                {
                    ViewBag.MyMessage = false;
                    ModelState.AddModelError("Mail", "نمی توانید از این ایمیل استفاده کنید");
                }
                else
                {
                    int userId = 0;
                    string mobileCode = "";

                    if (_account.ExistsMobileNumber(viewModel.Mobile))
                    {
                        _account.UpdateUserRole(viewModel.Mobile);

                        userId = _account.GetUserId(viewModel.Mobile);
                    }
                    else
                    {
                        mobileCode = CodeGenerators.ActiveCode();

                        User user = new User
                        {
                            ActiveCode = mobileCode,
                            Code = null,
                            Fullname = null,
                            IsActive = false,
                            Mobile = viewModel.Mobile,
                            Password = HashGenerators.MD5Encoding(viewModel.Password),
                            Date = pc.GetYear(DateTime.Now).ToString("0000") + "/" + pc.GetMonth(DateTime.Now).ToString("00") + "/"
                                    + pc.GetDayOfMonth(DateTime.Now).ToString("00"),
                            RoleId = _account.GetStoreRole()
                        };

                        _account.AddUser(user);

                        userId = user.Id;
                    }

                    Store store = new Store
                    {
                        Address = null,
                        Desc = null,
                        Logo = null,
                        Mail = viewModel.Mail,
                        MailActivate = false,
                        MobileActivate = false,
                        Tel = null,
                        UserId = userId,
                        Name = null,
                        MailActivateCode = CodeGenerators.ActiveCode()
                    };

                    _account.AddStrore(store);

                    ViewBag.MyMessage = true;

                    MessageSender sender = new MessageSender();
                    string messageBody = _render.RenderToStringAsync("_ActivateMail", store);

                    try
                    {
                        sender.SMS(viewModel.Mobile, "درخواست ثبت فروشگاه انجام شد"
                            + Environment.NewLine + "کد فعال سازی :" + mobileCode);

                        MessageSender.Email(store.Mail, "فعالسازی فروشگاه", messageBody);
                    }
                    catch 
                    {

                        
                    }
                }
            }

            return View(viewModel);
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/");
        }
    }
}
