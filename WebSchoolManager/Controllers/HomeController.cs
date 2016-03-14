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
            return Json(sm.RepForm.Get().Select(f => f.ToDto()), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPupils(int formId)
        {
            return Json(sm.RepPupil.Get(p => p.FormId == formId, "Form").Select(p => p.ToDto()), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPupilById(int pupilId)
        {
            return Json(sm.RepPupil.Get(p => p.PupilId == pupilId, "Form").Select(p => p.ToDto()), JsonRequestBehavior.AllowGet);
        }

        public void SavePupil(Pupil p, string formName)
        {
            p.FormId = sm.RepForm.Get(f => f.Name == formName).ToList()[0].FormId;
            sm.RepPupil.Update(p);
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
        /*
        public ActionResult SavePupil(Pupil p)
        {
            sm.RepPupil.Update(p);
            return RedirectToAction("Index", "Home", new IndexViewModel { SelectedForm = p.FormId });
        }*/

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
            else if ( submit == "Stundenplan anzeigen")
            {
                //sm.RepSubj.Create(new Subject { Description = "DBI" });
                //sm.RepSubj.Create(new Subject { Description = "NVS" });
                //sm.RepSubj.Create(new Subject { Description = "PROO" });
                for (var i = 1; i <= 10; i++)
                {
                    var lesson = sm.RepLes.Get(l => l.Unit == i);
                    for (var j = 1; j <= 5; j++)
                    {
                        lesson = lesson.Where(l => l.DayOfWeek == j);
                        lesson = lesson.Where(l => l.FormId == SelectedForm);
                        if (lesson.FirstOrDefault() == null)
                        {
                            sm.RepLes.Create(new Lesson
                            {
                                DayOfWeek = j,
                                Unit = i,
                                FormId = SelectedForm,
                                SubjectId = 1
                            });
                        }
                    }
                }
                var pm = new PlanModel
                {
                    SelectedForm = SelectedForm,
                    lessons = sm.RepLes.Get(),
                    subjects = sm.RepSubj.Get()
                };
                return PartialView("SubjectTable", pm);
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

        public ActionResult AddTest(int SelectedForm)
        {
            return View(new Test
            {
                FormId = SelectedForm
            });
        }

        public ActionResult SaveTest(Test t)
        {
            sm.RepTest.Create(t);

            return RedirectToAction("Index", new { SelectedForm = t.FormId });
        }

        public PartialViewResult MarkSelection(int SelectedForm, int testid,  int pupilid, int markValue)
        {
            sm.PersistMark(testid, pupilid, markValue);

            return PartialView("PartialMarkSelection", new MarkSelectionModel
            {
                SelectedForm = SelectedForm,
                Pupil = sm.RepPupil.Get(p => p.PupilId == pupilid, "Marks").FirstOrDefault(),
                TestId = testid
            });
        }

        public ActionResult ChangeMode(int SelectedForm)
        {
            foreach (var key in Request.Form.AllKeys)
            {
                string[] ids = key.Split('_');
                if (ids[0] == "subject")
                {
                    int hour = int.Parse(ids[1]);
                    int dateOfWeek = int.Parse(ids[2]);
                    string subjectDescription = Request.Form[key];
                    Subject subject = sm.RepSubj.Get(s => s.Description == subjectDescription).FirstOrDefault();
                    sm.PersistLesson(hour, dateOfWeek, subject, SelectedForm);
                }
            }
            return RedirectToAction("Index", new { SelectedForm = SelectedForm });
        }
    }
}