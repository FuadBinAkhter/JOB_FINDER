using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using JOB_FINDER.Data;
using JOB_FINDER.Models;

namespace JOB_FINDER.Controllers
{
    public class USERsController : Controller
    {
        private JobFinderDBEntities db = new JobFinderDBEntities();



        //private JobFinderDBContext

        // GET: USERs
        string name;
        int id;

        public ActionResult Index(string searching)
        {
            if (!String.IsNullOrWhiteSpace(searching))
            {
                return View(db.USERS.Where(x => x.Skill.Contains(searching)).ToList());
            }
            else
            {
                return View(db.USERS.ToList());
            }
        }
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]

        public ActionResult SignUp(USER uSER)
        {

            if (db.USERS.Any(x => x.Email == uSER.Email))
            {
                ViewBag.Notification = "An account with this email already exits";
            }
            else
            {
                db.USERS.Add(uSER);
                db.SaveChanges();
                Session["UserID"] = uSER.UserID.ToString();
                Session["Name"] = uSER.Name.ToString();
                Session["Password"] = uSER.Password.ToString();
                Session["Email"] = uSER.Email.ToString();
                return RedirectToAction("SignIn", "USERs");

            }
            return View();
        }

        [HttpGet]

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]

        public ActionResult SignIn(USER uSER)
        {
            var checklogin = db.USERS.Where(x => x.Email.Equals(uSER.Email) && x.Password.Equals(uSER.Password)).FirstOrDefault();

            if (checklogin != null)
            {
                Session["UserID"] = checklogin.UserID;
                id = uSER.UserID;
               
                Session["Email"] = uSER.Email.ToString();
             
                return RedirectToAction("ViewProfile", "USERs");
            }
            else
            {
                ViewBag.Notification = "Wrong email or password";
            }
            return View();
        }

        [OutputCache(NoStore = true, Duration = 60, VaryByParam = "*", Location = System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult SignOut()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("SignIn", "USERs");
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult ViewProfile()
        {
            if (Session["Email"] != null)
            {

                string userMail = Convert.ToString(Session["Email"]);

                var userDetails = db.USERS.Where(x => x.Email.Equals(userMail)).FirstOrDefault();

                if (userDetails.CV == null)
                {
                    ViewBag.Message = "No CV uploaded Yet";
                }
                else
                    ViewBag.Message = null;

                return View(userDetails);

            }
            return RedirectToAction("SignIn", "USERs");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult EditProfile(USER uSER, HttpPostedFileBase file)
        {
            string filename = null;
            int uid = 0;
            if (Session["Email"] != null)
            {
                string userMail = Convert.ToString(Session["Email"]);
                USER user = db.USERS.FirstOrDefault(u => u.Email.Equals(userMail));
                uid = user.UserID;
                filename = user.CV;
            }

            if (file != null && file.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Files"),
                                               Path.GetFileName("Uid_" + Convert.ToString(uid) + file.FileName));
                    file.SaveAs(path);

                    filename = "Uid_" + Convert.ToString(uid) + Path.GetFileName(file.FileName);


                    ViewBag.Message = path;
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }

            db.Configuration.ValidateOnSaveEnabled = false;
            if (Session["Email"] != null)
            {
                string userMail = Convert.ToString(Session["Email"]);
                USER user = db.USERS.FirstOrDefault(u => u.Email.Equals(userMail));

                user.Name = uSER.Name;
                user.Email = uSER.Email;
                user.Address = uSER.Address;
                user.Phone = uSER.Phone;
                if (uSER.Password == null)
                {
                    user.Password = user.Password;
                }
                else
                    user.Password = uSER.Password;

                user.University = uSER.University;
                user.Description = uSER.Description;
                user.Skill = uSER.Skill;

                if (filename == null)
                    user.CV = user.CV;
                else
                    user.CV = filename;

                db.Set<USER>().AddOrUpdate(user);

                db.SaveChanges();
                return RedirectToAction("ViewProfile", "USERs");
               
            }

            return RedirectToAction("SignOut", "USERs");
        }

        public ActionResult SeeApplicantProfile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(db.USERS.Where(x => x.UserID == id).ToList());
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult EditProfile()
        {

            string userMail = Convert.ToString(Session["Email"]);

            var userDetails = db.USERS.Where(x => x.Email.Equals(userMail)).FirstOrDefault();


            return View(userDetails);

        }

        public FileResult Download(string fileName)
        {

            string path = Path.Combine(Server.MapPath("~/Files/"), fileName);

            // set content type, e.g. 'application/pdf' for PDF files
            // or use 'System.Net.Mime.MediaTypeNames' to get predefined content types
            string contentType = MimeMapping.GetMimeMapping(path);

            // return FilePathResult to download
            return File(path, contentType, fileName);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        //For Final Checkpoint

        [HttpPost]
        public ActionResult ChangePassword(USER uSER)
        {
            //ModelState.Clear();
            db.Configuration.ValidateOnSaveEnabled = false;

            string usermail = Convert.ToString(uSER.Email);
            USER user = db.USERS.FirstOrDefault(u => u.Email.Equals(usermail));

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

                return RedirectToAction("SignIn", "USERs");
            }
            else
            {
                ViewBag.Notification = "Such email does not exist in server";
                return View();
            }
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
        public ActionResult UpdatePassword(USER uSER)
        {
            db.Configuration.ValidateOnSaveEnabled = false;
            if (Session["Email"] != null)
            {
                string userMail = Convert.ToString(Session["Email"]);
                var user = db.USERS.FirstOrDefault(u => u.Email.Equals(userMail));

                if(!user.Password.Equals(uSER.OldPassword))
                {
                    ViewBag.Notification = "Current Password is wrong";
                    return View();
                    
                }
                else if (uSER.Password.Length < 6 || uSER.ConfirmPassword.Length < 6)
                {
                    return View();
                }
                else if(uSER.Password!= uSER.ConfirmPassword)
                {
                    return View();
                }
                else
                {
                    user.Password = uSER.Password;
                    db.Set<USER>().AddOrUpdate(user);
                    db.SaveChanges();
                    return RedirectToAction("ViewProfile", "USERs");
                }

            }

            return RedirectToAction("SignOut", "USERs");
           
        }



   



        /* // GET: USERs/Details/5
         public ActionResult Details(int? id)
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
             return View(uSER);
         }

         // GET: USERs/Create
         public ActionResult Create()
         {
             return View();
         }

         // POST: USERs/Create
         // To protect from overposting attacks, enable the specific properties you want to bind to, for 
         // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Create([Bind(Include = "UserID,Name,Email,Phone,Address,Password,University,Description,Skill,CV")] USER uSER)
         {
             if (ModelState.IsValid)
             {
                 db.USERS.Add(uSER);
                 db.SaveChanges();
                 return RedirectToAction("Index");
             }

             return View(uSER);
         }

         // GET: USERs/Edit/5
         public ActionResult Edit(int? id)
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
             return View(uSER);
         }

         // POST: USERs/Edit/5
         // To protect from overposting attacks, enable the specific properties you want to bind to, for 
         // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Edit([Bind(Include = "UserID,Name,Email,Phone,Address,Password,University,Description,Skill,CV")] USER uSER)
         {
             if (ModelState.IsValid)
             {
                 db.Entry(uSER).State = EntityState.Modified;
                 db.SaveChanges();
                 return RedirectToAction("Index");
             }
             return View(uSER);
         }

         // GET: USERs/Delete/5
         public ActionResult Delete(int? id)
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
             return View(uSER);
         }

         // POST: USERs/Delete/5
         [HttpPost, ActionName("Delete")]
         [ValidateAntiForgeryToken]
         public ActionResult DeleteConfirmed(int id)
         {
             USER uSER = db.USERS.Find(id);
             db.USERS.Remove(uSER);
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
