using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TelefonRehberi_ok.Models;

namespace TelefonRehberi_ok.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Users U)
        {
            TRDBEntities db = new TRDBEntities();
            var count = db.Users.Where(x => x.username == U.username && x.pass == U.pass).Count();

            if(count == 0)
            {
                ViewBag.msg = "Kullanıcı Girişi Başarısız.";
                return View();
            }
            else
            {
                FormsAuthentication.SetAuthCookie(U.username, false);
                return RedirectToAction("Index", "Admin");
                //return Redirect(Request.UrlReferrer.ToString());
            }

        }
        [Authorize]//Nasıl Sadece kullanıcıların Basabileceği ve Url'ye gidebileceği Hale Getirebilir?
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Public");
        }
    }
}