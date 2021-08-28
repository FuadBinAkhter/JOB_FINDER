using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
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

        public ActionResult Index()
        {
            return View(db.USERS.ToList());
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
     
        public ActionResult SignUp(USER uSER)
        {

            if (db.USERS.Any(x => x.Name == uSER.Name))
            {
                ViewBag.Notification = "An account with this username already exits";
            }
            else
            {
                db.USERS.Add(uSER);
                db.SaveChanges();
                Session["UserID"] = uSER.UserID.ToString();
                Session["Name"] = uSER.Name.ToString();
                Session["Password"] = uSER.Password.ToString();
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
            var checklogin = db.USERS.Where(x => x.Name.Equals(uSER.Name) && x.Password.Equals(uSER.Password)).FirstOrDefault();

            if (checklogin != null)
            {
                Session["UserID"] = uSER.UserID.ToString();
                id = uSER.UserID;
                Session["Name"] = uSER.Name.ToString();
                name= uSER.Name.ToString();
                //return RedirectToAction("SignUp", "Home");
                return RedirectToAction("ViewProfile", "USERs");
            }
            else
            {
                ViewBag.Notification = "Wrong username or password";
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
            if (Session["Name"]!=null)
            {

                string userName = Convert.ToString(Session["Name"]);

                var userDetails = db.USERS.Where(x => x.Name.Equals(userName)).FirstOrDefault();

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
            string filename=null;
            int uid=0;
            if (Session["Name"] != null)
            {
                string userName = Convert.ToString(Session["Name"]);
                USER user = db.USERS.FirstOrDefault(u => u.Name.Equals(userName));
                uid = user.UserID;
                filename = user.CV;
            }
               
            if (file != null && file.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Files"),
                                               Path.GetFileName("Uid_"+Convert.ToString(uid)+"_"+file.FileName));
                    file.SaveAs(path);

                    filename = "Uid_"+Convert.ToString(uid)+"_"+Path.GetFileName(file.FileName);



                    /*if (Session["Name"] != null)
                    {
                        string userName = Convert.ToString(Session["Name"]);
                        USER user = db.USERS.FirstOrDefault(u => u.Name.Equals(userName));

                        user.Name = uSER.Name;
                        user.Email = uSER.Email;
                        user.Address = uSER.Address;

                        if (uSER.Password == null)
                        {
                            user.Password = user.Password;
                        }
                        else
                            user.Password = uSER.Password;

                        user.University = uSER.University;
                        user.Description = uSER.Description;
                        user.Skill = uSER.Skill;

                        if (Path.GetFileName(file.FileName) == null)
                            user.CV = user.CV;
                        else
                            user.CV = Path.GetFileName(file.FileName);

                        db.Set<USER>().AddOrUpdate(user);

                        db.SaveChanges();
                        //return RedirectToAction("ViewProfile", "USERs");
                        return View();
                    }*/

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
            if (Session["Name"]!=null)
            {
                string userName = Convert.ToString(Session["Name"]);
                USER user = db.USERS.FirstOrDefault(u => u.Name.Equals(userName));

                user.Name = uSER.Name;
                user.Email = uSER.Email;
                user.Address = uSER.Address;

                if(uSER.Password==null)
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
                return RedirectToAction("ViewProfile","USERs");
                //return View();
            }

            return RedirectToAction("SignOut","USERs");
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult EditProfile()
        {

            string userName = Convert.ToString(Session["Name"]);

            var userDetails = db.USERS.Where(x => x.Name.Equals(userName)).FirstOrDefault();
          

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



        /*[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FileUpload(HttpPostedFileBase uploadFile)
        {
            if (uploadFile.ContentLength > 0)
            {
                string filePath = Path.Combine(HttpContext.Server.MapPath("../Files"),
                                               Path.GetFileName(uploadFile.FileName));
                uploadFile.SaveAs(filePath);
            }
            return View();
        }
        */



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
