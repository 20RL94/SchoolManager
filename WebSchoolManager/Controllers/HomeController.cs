using DataSchoolManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSchoolManager.Models;

namespace WebSchoolManager.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        SchoolManager sm = new SchoolManager();

        public JsonResult GetForms()
        {
            return Json(sm.RepForm.Get()
                .Select(f => f.ToDto()),JsonRequestBehavior.AllowGet);
            
        }

        public JsonResult GetPupils(int formid)
        {
            return Json(sm.RepPupil.Get(p => p.FormId == formid)
                .Select(p => p.ToDto()), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Import(HttpPostedFileBase file)
        {
            if ( file != null && file.ContentLength > 0 )
                sm.Import(file.InputStream);
            return RedirectToAction("Index", new IndexViewModel());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public void SavePupil(Pupil p)
        {
            sm.RepPupil.Update(p);
        }

        public ActionResult SaveMarks(int SelectedForm)
        {
            foreach ( var key in Request.Form.AllKeys)
            {
                string[] ids = key.Split('_');
                if ( ids[0] == "mark")
                {
                    int testid = int.Parse(ids[1]);
                    int pupilid = int.Parse(ids[2]);
                    int mark = int.Parse(Request.Form[key]);
                    sm.PersistMark(testid, pupilid, mark);
                }
            }
            return RedirectToAction("Index", new { SelectedForm = SelectedForm });
        }
    }
}