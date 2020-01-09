using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TelefonRehberi_ok.Models;

namespace TelefonRehberi_ok.Controllers
{

    [AllowAnonymous]
    public class PublicController : Controller
    {
        private TRDBEntities db = new TRDBEntities();

        // GET: Public
        public ActionResult Index()
        {
            var eMPLOYEE = db.EMPLOYEE.Include(e => e.Department);
            return View(eMPLOYEE.ToList());
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
