using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
                    //This line added for CP 2
                    Session["CompanyID"] = company.CompanyID;
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
            if (Session["company_email"] != null)
            {
                string email = Convert.ToString(Session["company_email"]);
                var company = db.COMPANies.Where(c => c.Email.Equals(email)).FirstOrDefault();

                return View(company);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
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

        //For Final Checkpoint
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //For Final Checkpoint
        [HttpPost]
        public ActionResult ForgotPassword(COMPANY cOMPANY)
        {
            //ModelState.Clear();
            db.Configuration.ValidateOnSaveEnabled = false;

            string usermail = Convert.ToString(cOMPANY.Email);
            var user = db.COMPANies.FirstOrDefault(u => u.Email.Equals(usermail));

            if (user != null)
            {

                MailMessage mm = new MailMessage("job.finder.840@gmail.com", usermail/*txtEmail.Text.Trim()*/);
                mm.Subject = "Password Recovery";
                mm.Body = string.Format("Hi {0},<br /><br />Your password is {1}.<br /><br />Thank You.", user.Name, user.Password);
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential();
                NetworkCred.UserName = "job.finder.840@gmail.com";
                NetworkCred.Password = "jobfinder840@@";
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);



                return RedirectToAction("Login", "COMPANies");
            }
            else
            {
                ViewBag.Notification = "Such email does not exist in server";
                return View();
            }
            // return View();
        }

        //For Final Checkpoint
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult UpdatePassword()
        {
            return View();
        }

        //For Final Checkpoint
        [HttpPost]
        [ValidateAntiForgeryToken]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult UpdatePassword(COMPANY cOMPANY)
        {
            db.Configuration.ValidateOnSaveEnabled = false;
            if (Session["company_email"] != null)
            {
                string userMail = Convert.ToString(Session["company_email"]);
                var user = db.COMPANies.FirstOrDefault(u => u.Email.Equals(userMail));

                if (!user.Password.Equals(cOMPANY.OldPassword))
                {
                    ViewBag.Notification = "Current Password is wrong";
                    return View();

                }
                else if (cOMPANY.Password.Length < 6 || cOMPANY.ConfirmPassword.Length < 6)
                {
                    return View();
                }
                else if (cOMPANY.Password != cOMPANY.ConfirmPassword)
                {
                    return View();
                }
                else
                {
                    user.Password = cOMPANY.Password;
                    db.Set<COMPANY>().AddOrUpdate(user);
                    db.SaveChanges();
                    return RedirectToAction("CompanyProfile", "COMPANies");
                }

            }

            return RedirectToAction("Signout");

        }

        public ActionResult SeeCompanyProfile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(db.COMPANies.Where(x => x.CompanyID == id).ToList());
        }

        /*
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
        public ActionResult Create([Bind(Include = "CompanyID,Name,Description,Address,Phone,Email,Password")] COMPANY cOMPANY)
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
        public ActionResult Edit([Bind(Include = "CompanyID,Name,Description,Address,Phone,Email,Password")] COMPANY cOMPANY)
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
        }
        */
    }
}
