using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JOB_FINDER.Models;

namespace JOB_FINDER.Controllers
{
    public class ApplicationJoinedUserController : Controller
    {
        // GET: ApplicationJoinedUser
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobFinderDBEntities db = new JobFinderDBEntities();
            List<APPLICATION> application_date = db.APPLICATIONS.ToList();
            List<USER> user_name = db.USERS.ToList();
            var joinedTable = from a in application_date
                              join u in user_name on a.UserID equals u.UserID
                              where a.PostID == id
                              select new ApplicationJoinedUser { applicationDetails = a, userDetails = u };
            return View(joinedTable);
        }
    }
}