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

        public ActionResult Index(IndexViewModel ivm)
        {
            ivm.Forms = sm.RepForm.Get();
            ivm.Pupils = ivm.SelectedForm != null ? 
                  sm.RepPupil.Get(f => f.Form.FormId == ivm.SelectedForm) :
                  new Pupil[0];
            return View(ivm);
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

        public ActionResult EditPupil(int? pupilid)
        {
            if (pupilid != null)
            {
                Pupil p = sm.RepPupil.GetByKey(pupilid);
                ViewBag.Forms = sm.RepForm.Get();
                return View(p);
            }
            return RedirectToAction("Index", "Home", new IndexViewModel());
        }

        public ActionResult SavePupil(Pupil p)
        {
            sm.RepPupil.Update(p);
            return RedirectToAction("Index", "Home", new IndexViewModel { SelectedForm = p.FormId });
        }

        public PartialViewResult PupilTable(int SelectedForm, string submit)
        {
            if ( submit == "Tests anzeigen")
            {
                var tvm = new TestViewModel
                {
                    Pupils = sm.RepPupil.Get(p => p.FormId == SelectedForm, "Marks"),
                    Tests = sm.RepTest.Get(t => t.FormId == SelectedForm),
                    SelectedForm = SelectedForm
                };
                return PartialView("PartialTestTable", tvm);
            }
            else
                return PartialView("PartialPupilTable", sm.RepPupil.Get(p => p.FormId == SelectedForm));
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