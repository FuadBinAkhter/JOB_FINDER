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
    public class POSTsController : Controller
    {
        private JobFinderDBEntities db = new JobFinderDBEntities();

        // GET: POSTs
        // GET: POSTs/Create
        public ActionResult Create()
        {
            ViewBag.CompanyID = new SelectList(db.COMPANies, "CompanyID", "Name");
            return View();
        }
        // POST: POSTs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostID,CompanyID,Salary,Location,Experience,Description")] POST pOST)
        {
            if (ModelState.IsValid)
            {
                db.POSTs.Add(pOST);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyID = new SelectList(db.COMPANies, "CompanyID", "Name", pOST.CompanyID);
            return View(pOST);
        }
        public ActionResult Index(string searchByExp, string searchBySal, string searching)
        {
            if (searchByExp == "0-2")
            {
                if (!String.IsNullOrWhiteSpace(searching))
                {
                    if(searchBySal == "5,000-20,000")
                    {
                        return View(db.POSTs.Where(x => x.Experience >= 0 && x.Experience <= 2 && x.Salary >= 5000 && x.Salary <= 20000 && x.Location.Contains(searching)).ToList());
                    }
                    else if (searchBySal == "20,001-50,000")
                    {
                        return View(db.POSTs.Where(x => x.Experience >= 0 && x.Experience <= 2 && x.Salary > 20000 && x.Salary <= 50000 && x.Location.Contains(searching)).ToList());
                    }
                    else if (searchBySal == "50,000+")
                    {
                        return View(db.POSTs.Where(x => x.Experience >= 0 && x.Experience <= 2 && x.Salary > 50000 && x.Location.Contains(searching)).ToList());
                    }
                    else
                    {
                        return View(db.POSTs.Where(x => x.Experience >= 0 && x.Experience <= 2 && x.Location.Contains(searching)).ToList());
                    }
                }
                else
                {
                    if (searchBySal == "5,000-20,000")
                    {
                        return View(db.POSTs.Where(x => x.Experience >= 0 && x.Experience <= 2 && x.Salary >= 5000 && x.Salary <= 20000).ToList());
                    }
                    else if (searchBySal == "20,001-50,000")
                    {
                        return View(db.POSTs.Where(x => x.Experience >= 0 && x.Experience <= 2 && x.Salary > 20000 && x.Salary <= 50000).ToList());
                    }
                    else if (searchBySal == "50,000+")
                    {
                        return View(db.POSTs.Where(x => x.Experience >= 0 && x.Experience <= 2 && x.Salary > 50000).ToList());
                    }
                    else
                    {
                        return View(db.POSTs.Where(x => x.Experience >= 0 && x.Experience <= 2).ToList());
                    }
                }
            }
            else if (searchByExp == "3-5")
            {
                if (!String.IsNullOrWhiteSpace(searching))
                {
                    if (searchBySal == "5,000-20,000")
                    {
                        return View(db.POSTs.Where(x => x.Experience >= 3 && x.Experience <= 5 && x.Salary >= 5000 && x.Salary <= 20000 && x.Location.Contains(searching)).ToList());
                    }
                    else if (searchBySal == "20,001-50,000")
                    {
                        return View(db.POSTs.Where(x => x.Experience >= 3 && x.Experience <= 5 && x.Salary > 20000 && x.Salary <= 50000 && x.Location.Contains(searching)).ToList());
                    }
                    else if (searchBySal == "50,000+")
                    {
                        return View(db.POSTs.Where(x => x.Experience >= 3 && x.Experience <= 5 && x.Salary > 50000 && x.Location.Contains(searching)).ToList());
                    }
                    else
                    {
                        return View(db.POSTs.Where(x => x.Experience >= 3 && x.Experience <= 5 && x.Location.Contains(searching)).ToList());
                    }
                }
                else
                {
                    if (searchBySal == "5,000-20,000")
                    {
                        return View(db.POSTs.Where(x => x.Experience >= 3 && x.Experience <= 5 && x.Salary >= 5000 && x.Salary <= 20000).ToList());
                    }
                    else if (searchBySal == "20,001-50,000")
                    {
                        return View(db.POSTs.Where(x => x.Experience >= 3 && x.Experience <= 5 && x.Salary > 20000 && x.Salary <= 50000).ToList());
                    }
                    else if (searchBySal == "50,000+")
                    {
                        return View(db.POSTs.Where(x => x.Experience >= 3 && x.Experience <= 5 && x.Salary > 50000).ToList());
                    }
                    else
                    {
                        return View(db.POSTs.Where(x => x.Experience >= 3 && x.Experience <= 5).ToList());
                    }
                }
            }
            else if (searchByExp == "6-10")
            {
                if (!String.IsNullOrWhiteSpace(searching))
                {
                    if (searchBySal == "5,000-20,000")
                    {
                        return View(db.POSTs.Where(x => x.Experience >= 6 && x.Experience <= 10 && x.Salary >= 5000 && x.Salary <= 20000 && x.Location.Contains(searching)).ToList());
                    }
                    else if (searchBySal == "20,001-50,000")
                    {
                        return View(db.POSTs.Where(x => x.Experience >= 6 && x.Experience <= 10 && x.Salary > 20000 && x.Salary <= 50000 && x.Location.Contains(searching)).ToList());
                    }
                    else if (searchBySal == "50,000+")
                    {
                        return View(db.POSTs.Where(x => x.Experience >= 6 && x.Experience <= 10 && x.Salary > 50000 && x.Location.Contains(searching)).ToList());
                    }
                    else
                    {
                        return View(db.POSTs.Where(x => x.Experience >= 6 && x.Experience <= 10 && x.Location.Contains(searching)).ToList());
                    }
                }
                else
                {
                    if (searchBySal == "5,000-20,000")
                    {
                        return View(db.POSTs.Where(x => x.Experience >= 6 && x.Experience <= 10 && x.Salary >= 5000 && x.Salary <= 20000).ToList());
                    }
                    else if (searchBySal == "20,001-50,000")
                    {
                        return View(db.POSTs.Where(x => x.Experience >= 6 && x.Experience <= 10 && x.Salary > 20000 && x.Salary <= 50000).ToList());
                    }
                    else if (searchBySal == "50,000+")
                    {
                        return View(db.POSTs.Where(x => x.Experience >= 6 && x.Experience <= 10 && x.Salary > 50000).ToList());
                    }
                    else
                    {
                        return View(db.POSTs.Where(x => x.Experience >= 6 && x.Experience <= 10).ToList());
                    }
                }
            }
            else
            {
                if (!String.IsNullOrWhiteSpace(searching))
                {
                    if (searchBySal == "5,000-20,000")
                    {
                        return View(db.POSTs.Where(x => x.Salary >= 5000 && x.Salary <= 20000 && x.Location.Contains(searching)).ToList());
                    }
                    else if (searchBySal == "20,001-50,000")
                    {
                        return View(db.POSTs.Where(x => x.Salary > 20000 && x.Salary <= 50000 && x.Location.Contains(searching)).ToList());
                    }
                    else if (searchBySal == "50,000+")
                    {
                        return View(db.POSTs.Where(x => x.Salary > 50000 && x.Location.Contains(searching)).ToList());
                    }
                    else
                    {
                        return View(db.POSTs.Where(x => x.Location.Contains(searching)).ToList());
                    }
                }
                else
                {
                    if (searchBySal == "5,000-20,000")
                    {
                        return View(db.POSTs.Where(x => x.Salary >= 5000 && x.Salary <= 20000).ToList());
                    }
                    else if (searchBySal == "20,001-50,000")
                    {
                        return View(db.POSTs.Where(x => x.Salary > 20000 && x.Salary <= 50000).ToList());
                    }
                    else if (searchBySal == "50,000+")
                    {
                        return View(db.POSTs.Where(x => x.Salary > 50000).ToList());
                    }
                    else
                    {
                        return View(db.POSTs.ToList());
                    }
                }
            }
        }

        /*Apply method added for CP 2*/
        public ActionResult Apply(int? id)
        {
            if (Session["UserID"] == null)
            {
                ViewBag.Notification = "Please login first!";
            }
            else
            {
                APPLICATION aPPLICATION = new APPLICATION();
                aPPLICATION.ApplicationDate = DateTime.Today;
                aPPLICATION.PostID = (int)id;
                aPPLICATION.UserID = (int)Session["UserID"];

                db.APPLICATIONS.Add(aPPLICATION);
                db.SaveChanges();
                ViewBag.Notification = "Application confirmed!";
            }

            return View();
        }

        /*
        public ActionResult Index(string searching)
        {
            int exp;
            if (String.IsNullOrWhiteSpace(searching))
            {
                exp = 0;
            }
            else
            {
                exp = Convert.ToInt32(searching);
            }
            return View(db.POSTs.Where(x => x.Experience == exp || searching == null).ToList());
        }
        */

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
