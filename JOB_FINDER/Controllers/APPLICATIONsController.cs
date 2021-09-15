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
    public class APPLICATIONsController : Controller
    {
        private JobFinderDBEntities db = new JobFinderDBEntities();

        // GET: APPLICATIONs
        public ActionResult Index()
        {
            var aPPLICATIONS = db.APPLICATIONS.Include(a => a.POST).Include(a => a.USER);
            return View(aPPLICATIONS.ToList());
        }

        // GET: APPLICATIONs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            APPLICATION aPPLICATION = db.APPLICATIONS.Find(id);
            if (aPPLICATION == null)
            {
                return HttpNotFound();
            }
            return View(aPPLICATION);
        }

        // GET: APPLICATIONs/Create
        public ActionResult Create()
        {
            ViewBag.PostID = new SelectList(db.POSTs, "PostID", "Location");
            ViewBag.UserID = new SelectList(db.USERS, "UserID", "Name");
            return View();
        }

        // POST: APPLICATIONs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApplicationID,UserID,PostID,ApplicationDate")] APPLICATION aPPLICATION)
        {
            if (ModelState.IsValid)
            {
                db.APPLICATIONS.Add(aPPLICATION);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PostID = new SelectList(db.POSTs, "PostID", "Location", aPPLICATION.PostID);
            ViewBag.UserID = new SelectList(db.USERS, "UserID", "Name", aPPLICATION.UserID);
            return View(aPPLICATION);
        }

        // GET: APPLICATIONs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            APPLICATION aPPLICATION = db.APPLICATIONS.Find(id);
            if (aPPLICATION == null)
            {
                return HttpNotFound();
            }
            ViewBag.PostID = new SelectList(db.POSTs, "PostID", "Location", aPPLICATION.PostID);
            ViewBag.UserID = new SelectList(db.USERS, "UserID", "Name", aPPLICATION.UserID);
            return View(aPPLICATION);
        }

        // POST: APPLICATIONs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApplicationID,UserID,PostID,ApplicationDate")] APPLICATION aPPLICATION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aPPLICATION).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PostID = new SelectList(db.POSTs, "PostID", "Location", aPPLICATION.PostID);
            ViewBag.UserID = new SelectList(db.USERS, "UserID", "Name", aPPLICATION.UserID);
            return View(aPPLICATION);
        }

        // GET: APPLICATIONs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            APPLICATION aPPLICATION = db.APPLICATIONS.Find(id);
            if (aPPLICATION == null)
            {
                return HttpNotFound();
            }
            return View(aPPLICATION);
        }

        // POST: APPLICATIONs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            APPLICATION aPPLICATION = db.APPLICATIONS.Find(id);
            db.APPLICATIONS.Remove(aPPLICATION);
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
