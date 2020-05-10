using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Exam__.Models;
using System.Net;

namespace Exam__.Controllers
{
    public class ExamController : Controller
    {
        // GET: Exam
        public ActionResult Index()
        {
            SipminiEntities2 DB = new SipminiEntities2();

            List<subject> subjectList = DB.subjects.ToList();
            ViewBag.subList = new SelectList(subjectList, "subject_id", "subject1");

            List<grade> gradeList = DB.grades.ToList();
            ViewBag.grList = new SelectList(gradeList, "grade_id", "grade1");

            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file, ExamUpload ex)
        {

            if (!ModelState.IsValid)
            {


                return new JsonResult { Data = "File not upload" };


            }
            else
            {
                SipminiEntities2 DB = new SipminiEntities2();
                Exam2 log = new Exam2();

                var subject = DB.subjects.Where(m => m.subject_id == ex.subject_id).Select(u => new { subject = u.subject1}).Single();
                var grade = DB.grades.Where(m => m.grade_id == ex.grade_id).Select(u => new { grade = u.grade1 }).Single();

                string path = Server.MapPath("~/App_Data/File");
                string fileName = Path.GetFileName(file.FileName);

                string fullPath = Path.Combine(path, fileName);

                log.FileName = fileName;
                log.FilePath = fullPath;
                log.Grade = grade.grade;
                log.Subject = subject.subject;
                log.TeacherName = "ASDF";
                

                DB.Exam2.Add(log);
                DB.SaveChanges();


                file.SaveAs(fullPath);
                return new JsonResult { Data = "File uploaded" };
            }
       }

        public ActionResult ViewList()
        {
            SipminiEntities2 db = new SipminiEntities2();

            List<Exam2> files = db.Exam2.ToList();

            return View(files);

        }
        public FileResult DownloadFile(string fileName)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(@"C:\Users\Halo\source\repos\Exam__\Exam__\App_Data\File" + fileName);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);


        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SipminiEntities2 db = new SipminiEntities2();
            Exam2 file = db.Exam2.Find(id);

            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);

        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSucces(int id)
        {
            SipminiEntities2 db = new SipminiEntities2();
            Exam2 file = db.Exam2.Find(id);


            Exam2 teacher = db.Exam2.Find(id);
            db.Exam2.Remove(teacher);
            db.SaveChanges();

            


            var directory = new DirectoryInfo(Server.MapPath("~/App_Data/File"));
            string getFile = directory.FullName + "\\" + file.FileName;
            System.IO.File.Delete(getFile);

            db.Exam2.Remove(file);
            db.SaveChanges();

            return RedirectToAction("ViewList");
        }

    }
}