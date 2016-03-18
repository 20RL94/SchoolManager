using DataSchoolManager;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSchoolManager.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        SchoolManager sm = new SchoolManager();

        public JsonResult GetForms()
        {
            return Json(sm.RepForm.Get()
                          .Select(f => new { FormId = f.FormId,
                                             Name = f.Name }),
                                   JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPupilById(int pupilId)
        {
            return Json(sm.RepPupil.Get(p => p.PupilId == pupilId)
                          .Select(p => new
                          {
                              PupilId = p.PupilId,
                              MatrikelNo = p.MatrikelNo,
                              FirstName = p.Firstname,
                              LastName = p.Lastname,
                              Birthday = p.Birthday,
                              Sex = p.Sex,
                              FormId = p.FormId
                          }).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPupilsByForm(int formid)
        {
            return Json(sm.RepPupil.Get(p => p.FormId == formid)
                          .Select(p => new
                          {
                              PupilId = p.PupilId,
                              MatrikelNo = p.MatrikelNo,
                              FirstName = p.Firstname,
                              LastName = p.Lastname,
                              Birthday = p.Birthday,
                              Sex = p.Sex,
                              FormId = p.FormId
                          }), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPupilsByPattern(string pattern)
        {
            return Json(sm.RepPupil.Get(p => p.Lastname.ToLower().StartsWith(pattern.ToLower())||
                                             p.Lastname.ToLower().StartsWith(pattern.ToLower()))
                          .Select(p => new
                          {
                              PupilId = p.PupilId,
                              FirstName = p.Firstname,
                              LastName = p.Lastname,
                          }), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void SavePupil(Pupil pupil)
        {
            Pupil p = sm.RepPupil.GetByKey(pupil.PupilId);
            if (p != null)
                sm.RepPupil.Update(pupil);
            else
                sm.RepPupil.Create(pupil);
        }

        public ActionResult Index()
        {
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
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public ActionResult Import(HttpPostedFileBase file)
        {
            if ( file != null && file.ContentLength > 0 )
                sm.Import(file.InputStream);
            return RedirectToAction("Index");
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
    }
}