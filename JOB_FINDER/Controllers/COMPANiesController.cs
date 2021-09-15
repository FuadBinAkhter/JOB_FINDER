using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JOB_FINDER.Data;
using JOB_FINDER.Models;

namespace JOB_FINDER.Controllers
{
    public class COMPANiesController : Controller
    {
        private JobFinderDBEntities db = new JobFinderDBEntities();

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(COMPANY company)
        {
            if (ModelState.IsValid)
            {
                db.COMPANies.Add(company);
                db.SaveChanges();

                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(TempCompany tempcompany)
        {
            if (ModelState.IsValid)
            {
                var company = db.COMPANies.Where(c => c.Email.Equals(tempcompany.Email) && c.Password.Equals(tempcompany.Password)).FirstOrDefault();

                if (company != null)
                {
                    Session["company_email"] = company.Email;
                    return RedirectToAction("CompanyProfile");
                }
                else
                {
                    ViewBag.LoginFailed = "User not found or password mismatched";
                    return View();
                }
            }
            return View();
        }

        public ActionResult CompanyProfile()
        {
            string email = Convert.ToString(Session["company_email"]);
            var company = db.COMPANies.Where(c => c.Email.Equals(email)).FirstOrDefault();

            return View(company);
        }

        //signout added
        public ActionResult SignOut()
        {
            Session["company_email"] = null;
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SeePrevPostst(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMPANY cOMPANY = db.COMPANies.Find(id);

            if (cOMPANY == null)
            {
                return HttpNotFound();
            }
            return View(db.POSTs.Where(x => x.CompanyID == id).ToList());
            //return View(cOMPANY);
        }


        /*private Job_FinderContext db = new Job_FinderContext();

        // GET: COMPANies
        public ActionResult Index()
        {
            return View(db.COMPANies.ToList());
        }

        // GET: COMPANies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMPANY cOMPANY = db.COMPANies.Find(id);
            if (cOMPANY == null)
            {
                return HttpNotFound();
            }
            return View(cOMPANY);
        }

        // GET: COMPANies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: COMPANies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompanyID,Name,Description,Address,Phone,Email")] COMPANY cOMPANY)
        {
            if (ModelState.IsValid)
            {
                db.COMPANies.Add(cOMPANY);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cOMPANY);
        }

        // GET: COMPANies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMPANY cOMPANY = db.COMPANies.Find(id);
            if (cOMPANY == null)
            {
                return HttpNotFound();
            }
            return View(cOMPANY);
        }

        // POST: COMPANies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompanyID,Name,Description,Address,Phone,Email")] COMPANY cOMPANY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOMPANY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cOMPANY);
        }

        // GET: COMPANies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMPANY cOMPANY = db.COMPANies.Find(id);
            if (cOMPANY == null)
            {
                return HttpNotFound();
            }
            return View(cOMPANY);
        }

        // POST: COMPANies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            COMPANY cOMPANY = db.COMPANies.Find(id);
            db.COMPANies.Remove(cOMPANY);
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
        }*/
    }
}
