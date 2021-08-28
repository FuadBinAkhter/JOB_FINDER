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
