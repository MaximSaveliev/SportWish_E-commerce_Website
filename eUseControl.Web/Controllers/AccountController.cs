using eUseControl.BusinessLogic;
using eUseControl.BusinessLogic.DBModel;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.BusinessLogic.Services;
using eUseControl.Controllers;
using eUseControl.Domain.Entities;
using eUseControl.Domain.Enums;
using eUseControl.Web.ActionAtributes;
using eUseControl.Web.Controllers;
using eUseControl.Web.Extensions;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAplication.Models;
using static eUseControl.BusinessLogic.Core.SessionApi;

namespace WebAplication.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ISession _session;
        private readonly IUser _user;

        public AccountController()
        {
            var bl = new BusinessLogic();
            _user = bl.GetUsertBL();
            _session = bl.GetSessionBL();
        }

        // GET: Account
        [HttpGet, AuthorizedMod]
        public ActionResult Index()
        {
            var user = System.Web.HttpContext.Current.GetMySessionObject();
            if (user == null)
            {
                return HttpNotFound();
            }
            else
            {
                User UserDB = new User
                {
                    NickName = user.NickName,
                    UserName = user.UserName,
                    UserSurname = user.UserSurname,
                    Email = user.Email,
                    AccessLevel = user.AccessLevel,
                };
                return View(UserDB);
            }
        }

        public ActionResult Index(int id)
        {
            return View(_user.GetUserById(id));
        }

        public ActionResult SignIn()
        {
            ViewBag.Message = "Login Page";
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(UserLoginForm data)
        {
            if (ModelState.IsValid)
            {
                LoginData uLogin = new LoginData
                {
                    NickName = data.NickName,
                    Password = data.Password,
                    Time = DateTime.Now
                };

                var response = _session.ValidateUserCredential(uLogin);
                SessionStatus();
                if (response.Status)
                {
                    var cookieResponse = _session.GenCookie(data.NickName);
                    if (cookieResponse != null)
                    {
                        ControllerContext.HttpContext.Response.Cookies.Add(cookieResponse.Cookie);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    ViewBag.Error = "Invalid NickName or password.";
                    ModelState.AddModelError("Invalid NickName or password.", response.StatusMessage);
                    ViewData["LoginFlag"] = "Invalid NickName or password.";
                    return View();
                }
            }
            return View();
        }

        public ActionResult SignUp()
        {
            ViewBag.Message = "Register Page";
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult SignUp(UserRegisterForm data)
        {
            if (ModelState.IsValid)
            {
                RegisterData uRegister = new RegisterData
                {
                    NickName = data.NickName,
                    UserName = data.UserName,
                    UserSurname = data.UserSurname,
                    Email = data.Email,
                    Password = data.Password,
                    //Time = DateTime.Now
                };

                var response = _session.ValidateUserRegister(uRegister);
                if (response.Status)
                {
                    return RedirectToAction("SignIn", "Account");
                }
                else
                {
                    ModelState.AddModelError("", response.StatusMessage);
                    return View(data);
                }
            }
            return View();// Проверить 
        }

        [AuthorizedMod]
        public ActionResult Logout()
        {
            System.Web.HttpContext.Current.Session.Clear();
            if (ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("X-KEY"))
            {
                var cookie = ControllerContext.HttpContext.Request.Cookies["X-KEY"];
                if (cookie != null)
                {
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                }
            }
            System.Web.HttpContext.Current.Session["LoginStatus"] = "logout";
            return RedirectToAction("Index", "Home");
        }

        [AuthorizedMod]
        public ActionResult EditUser(int id)
        {
            if (System.Web.HttpContext.Current.GetMySessionObject().AccessLevel == URole.ADMINISTRATOR)
            {
                var user = _user.GetUserById(id);
                if (user == null)
                {
                    return RedirectToAction("ListUsers", "Admin");
                }
                else
                {
                    return View(_user.GetUserById(id));
                }
            }

            else if (System.Web.HttpContext.Current.GetMySessionObject().AccessLevel == URole.USER)
            {
                var db = new UserContext();
                var user = System.Web.HttpContext.Current.GetMySessionObject();
                if (user.Id != id)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(_user.GetUserById(id));
                }
            }

            else
            {
                return HttpNotFound();
            }
        }

        [AuthorizedMod]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(User editUser)
        {
            var sessionObject = System.Web.HttpContext.Current.GetMySessionObject();
            var response = _user.ValidateEditUser(editUser);
            if (response.Status)
            {
                if (sessionObject.Id == editUser.Id)
                {
                    sessionObject.NickName = editUser.NickName;
                    sessionObject.UserName = editUser.UserName;
                    sessionObject.UserSurname = editUser.UserSurname;
                    sessionObject.Email = editUser.Email;
                    sessionObject.AccessLevel = editUser.AccessLevel;

                    var cookieResponse = _session.GenCookie(sessionObject.Email);
                    if (cookieResponse != null)
                    {
                        ControllerContext.HttpContext.Response.Cookies.Add(cookieResponse.Cookie);
                        return RedirectToAction("ListUsers", "Admin");
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    return RedirectToAction("ListUsers", "Admin");
                }
            }
            else
            {
                ModelState.AddModelError("NickName or email already exists", response.StatusMessage);
                return View(editUser);
            }
        }
    }
}