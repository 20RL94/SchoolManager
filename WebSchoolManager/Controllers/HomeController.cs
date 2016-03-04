using DataSchoolManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSchoolManager.Models;

namespace WebSchoolManager.Controllers
{
   // [Authorize]
    public class HomeController : Controller
    {
        SchoolManager sm = new SchoolManager();

        public JsonResult GetForms()
        {
            return Json(sm.RepForm.Get().Select(f => new {
                FormId = f.FormId,
                Name = f.Name
            }), JsonRequestBehavior.AllowGet);
        }

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
                sm.Import(file.FileName);
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
                ViewBag.Forms = sm.RepForm.Get(); // ViewBag ... dynamischer Datentyp
                return View(p);
            }
            return RedirectToAction("Index", "Home", new IndexViewModel()); // Umleitung zum Index
        }
        
        public ActionResult SavePupil(Pupil p)
        {
            sm.RepPupil.Update(p);
            return RedirectToAction("Index", "Home", new IndexViewModel { SelectedForm = p.FormId });
        }

        public PartialViewResult PupilTable(int SelectedForm) 
        {
            return PartialView("PartialPupilTable", 
                               sm.RepPupil.Get(p => p.FormId == SelectedForm));
        }
    }
}