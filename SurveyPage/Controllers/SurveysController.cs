using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SurveyPage.Models;

using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using SurveyPage.ViewModels;

namespace SurveyPage.Controllers
{
    [Authorize]
    public class SurveysController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Surveys
        public ActionResult Index()
        {
            return View(db.Surveys.ToList());
        }

        // GET: Surveys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        // GET: Surveys/Create
        public ActionResult Create()
        {
            SurveyQuestionViewModel surveyModel = new SurveyQuestionViewModel();
            List<SurveyQuestionViewModel> surveyModelList = new List<SurveyQuestionViewModel>();
            surveyModelList.Add(surveyModel);
            return View(surveyModelList);
        }

        //POST: Surveys/Create
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IEnumerable<SurveyQuestionViewModel> questions)
        {
            Survey survey = new Survey();
            foreach (var item in questions)
            {
                Question question = new Question();
                question.SurveyQuestion = item.SurveyQuestion.ToString();
                question.Survey = survey;
                db.Questions.Add(question);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //public async Task<ActionResult> Create([Bind(Include = "Questions")] Survey survey, IEnumerable<Question> questions)
        //{
        //    var currentUser = db.Users.Find(User.Identity.GetUserId());
        //    if (ModelState.IsValid)
        //    {                
        //        //survey.User = currentUser;
        //        //db.Surveys.Add(survey);
        //        //await db.SaveChangesAsync();  This needs Modification
        //        //db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    //return View(survey);
        //    return View();
        //}

        // GET: Surveys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        // POST: Surveys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Expertise,Professionalism,Accountability")] Survey survey)
        {
            if (ModelState.IsValid)
            {
                db.Entry(survey).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(survey);
        }

        // GET: Surveys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        // POST: Surveys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Survey survey = db.Surveys.Find(id);
            db.Surveys.Remove(survey);
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
        }
    }
}
