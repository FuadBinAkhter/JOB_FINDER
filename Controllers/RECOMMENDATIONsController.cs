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
    public class RECOMMENDATIONsController : Controller
    {
        private JobFinderDBEntities db = new JobFinderDBEntities();

        // GET: RECOMMENDATIONs
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USERS.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(db.RECOMMENDATIONs.Where(x => x.UserID == id).ToList());
            //var rECOMMENDATIONs = db.RECOMMENDATIONs.Include(r => r.COMPANY).Include(r => r.USER);
            //return View(rECOMMENDATIONs.ToList());
        }

        // GET: RECOMMENDATIONs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RECOMMENDATION rECOMMENDATION = db.RECOMMENDATIONs.Find(id);
            if (rECOMMENDATION == null)
            {
                return HttpNotFound();
            }
            return View(rECOMMENDATION);
        }

        // GET: RECOMMENDATIONs/Create
        public ActionResult Create(int? id)
        {
            USER uSER = db.USERS.Find(id);
            ViewBag.UserName = uSER.Name;

            ViewBag.UserID = id;
            ViewBag.CompanyID = Session["CompanyID"];
            ViewBag.Date = DateTime.Today.ToString("d");
            return View();
        }

        // POST: RECOMMENDATIONs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecommendationID,CompanyID,UserID,Description,RecommendationDate")] RECOMMENDATION rECOMMENDATION)
        {
            if (ModelState.IsValid)
            {
                db.RECOMMENDATIONs.Add(rECOMMENDATION);
                db.SaveChanges();
                return RedirectToAction("SeeApplicantProfile", "USERs", new { id = rECOMMENDATION.UserID });
            }

            ViewBag.CompanyID = new SelectList(db.COMPANies, "CompanyID", "Name", rECOMMENDATION.CompanyID);
            ViewBag.UserID = new SelectList(db.USERS, "UserID", "Name", rECOMMENDATION.UserID);
            return View(rECOMMENDATION);
        }

        // GET: RECOMMENDATIONs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RECOMMENDATION rECOMMENDATION = db.RECOMMENDATIONs.Find(id);
            if (rECOMMENDATION == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyID = new SelectList(db.COMPANies, "CompanyID", "Name", rECOMMENDATION.CompanyID);
            ViewBag.UserID = new SelectList(db.USERS, "UserID", "Name", rECOMMENDATION.UserID);
            return View(rECOMMENDATION);
        }

        // POST: RECOMMENDATIONs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecommendationID,CompanyID,UserID,Description,RecommendationDate")] RECOMMENDATION rECOMMENDATION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rECOMMENDATION).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyID = new SelectList(db.COMPANies, "CompanyID", "Name", rECOMMENDATION.CompanyID);
            ViewBag.UserID = new SelectList(db.USERS, "UserID", "Name", rECOMMENDATION.UserID);
            return View(rECOMMENDATION);
        }

        // GET: RECOMMENDATIONs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RECOMMENDATION rECOMMENDATION = db.RECOMMENDATIONs.Find(id);
            if (rECOMMENDATION == null)
            {
                return HttpNotFound();
            }
            return View(rECOMMENDATION);
        }

        // POST: RECOMMENDATIONs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RECOMMENDATION rECOMMENDATION = db.RECOMMENDATIONs.Find(id);
            db.RECOMMENDATIONs.Remove(rECOMMENDATION);
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
