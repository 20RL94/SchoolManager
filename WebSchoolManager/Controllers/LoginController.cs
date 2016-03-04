using DataSchoolManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebSchoolManager.Models;

namespace WebSchoolManager.Controllers
{
    public class LoginController : Controller
    {
        SchoolManager sm = new SchoolManager();

        public ActionResult Index(LoginViewModel lvm)
        {
            if ( lvm.Username != null )
            {
                Pupil pupil = sm.RepPupil.Get(p => p.Lastname == lvm.Username).FirstOrDefault();
                if ( pupil != null )
                {
                    Session["user"] = pupil;
                    FormsAuthentication.SetAuthCookie(pupil.Lastname, false);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(lvm);
        }
    }
}