using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JOB_FINDER.Models;

namespace JOB_FINDER.Controllers
{
    public class REVIEWsController : Controller
    {
        private JobFinderDBEntities db = new JobFinderDBEntities();

        // GET: REVIEWs
        public ActionResult Index()
        {
            var rEVIEWs = db.REVIEWs.Include(r => r.COMPANY).Include(r => r.USER);
            return View(rEVIEWs.ToList());
        }

        // GET: REVIEWs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REVIEW rEVIEW = db.REVIEWs.Find(id);
            if (rEVIEW == null)
            {
                return HttpNotFound();
            }
            return View(rEVIEW);
        }

        // GET: REVIEWs/Create
        public ActionResult Create()
        {
            ViewBag.CompanyID = new SelectList(db.COMPANies, "CompanyID", "Name");
            ViewBag.UserID = new SelectList(db.USERS, "UserID", "Name");
            return View();
        }

        // POST: REVIEWs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReviewID,UserID,CompanyID,ReviewDate,Description")] REVIEW rEVIEW)
        {
            if (ModelState.IsValid)
            {
                db.REVIEWs.Add(rEVIEW);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyID = new SelectList(db.COMPANies, "CompanyID", "Name", rEVIEW.CompanyID);
            ViewBag.UserID = new SelectList(db.USERS, "UserID", "Name", rEVIEW.UserID);
            return View(rEVIEW);
        }

        // GET: REVIEWs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REVIEW rEVIEW = db.REVIEWs.Find(id);
            if (rEVIEW == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyID = new SelectList(db.COMPANies, "CompanyID", "Name", rEVIEW.CompanyID);
            ViewBag.UserID = new SelectList(db.USERS, "UserID", "Name", rEVIEW.UserID);
            return View(rEVIEW);
        }

        // POST: REVIEWs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReviewID,UserID,CompanyID,ReviewDate,Description")] REVIEW rEVIEW)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rEVIEW).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyID = new SelectList(db.COMPANies, "CompanyID", "Name", rEVIEW.CompanyID);
            ViewBag.UserID = new SelectList(db.USERS, "UserID", "Name", rEVIEW.UserID);
            return View(rEVIEW);
        }

        // GET: REVIEWs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REVIEW rEVIEW = db.REVIEWs.Find(id);
            if (rEVIEW == null)
            {
                return HttpNotFound();
            }
            return View(rEVIEW);
        }

        // POST: REVIEWs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            REVIEW rEVIEW = db.REVIEWs.Find(id);
            db.REVIEWs.Remove(rEVIEW);
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
