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

    [Authorize]
    public class AdminController : Controller
    {
        private TRDBEntities db = new TRDBEntities();

        
        // GET: Admin
        public ActionResult Index(string q)
        {
            var eMPLOYEE = db.EMPLOYEE.Include(e => e.Department);

            if (!String.IsNullOrEmpty(q))
            {
                q = q.ToLower();
                eMPLOYEE = db.EMPLOYEE.Where(i => i.fname.ToLower().Contains(q) || i.sname.ToLower().Contains(q));
            }
            return View(eMPLOYEE.ToList());
        }

        

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLOYEE eMPLOYEE = db.EMPLOYEE.Find(id);
            if (eMPLOYEE == null)
            {
                return HttpNotFound();
            }
            return View(eMPLOYEE);
        }

        
        // GET: Admin/Create
        public ActionResult Create()
        {
            ViewBag.dep_id = new SelectList(db.Department, "dep_id", "dep_name");
            ViewBag.mng_id = new SelectList(db.Emp_Man, "man_id", "man_id");
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "emp_id,fname,sname,mng_id,dep_id,tel")] EMPLOYEE eMPLOYEE)
        {
            if (ModelState.IsValid)
            {
                db.EMPLOYEE.Add(eMPLOYEE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.dep_id = new SelectList(db.Department, "dep_id", "dep_name", eMPLOYEE.dep_id);
            return View(eMPLOYEE);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLOYEE eMPLOYEE = db.EMPLOYEE.Find(id);
            if (eMPLOYEE == null)
            {
                return HttpNotFound();
            }
            ViewBag.dep_id = new SelectList(db.Department, "dep_id", "dep_name", eMPLOYEE.dep_id);
            ViewBag.mng_id = new SelectList(db.Emp_Man, "man_id", "man_id", eMPLOYEE.mng_id);
            return View(eMPLOYEE);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "emp_id,fname,sname,mng_id,dep_id,tel")] EMPLOYEE eMPLOYEE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eMPLOYEE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.dep_id = new SelectList(db.Department, "dep_id", "dep_name", eMPLOYEE.dep_id);
            return View(eMPLOYEE);
        }

        // GET: EMPLOYEEs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLOYEE eMPLOYEE = db.EMPLOYEE.Find(id);
            if (eMPLOYEE == null)
            {
                return HttpNotFound();
            }
            return View(eMPLOYEE);
        }

        // POST: EMPLOYEEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var emp = db.EMPLOYEE.Where(i => i.emp_id == id);
            var em = db.Emp_Man.Where(i => i.emp_id == id);
            db.EMPLOYEE.RemoveRange(emp);

            db.Emp_Man.RemoveRange(em);
            db.SaveChanges();
            return RedirectToAction("Index");
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
