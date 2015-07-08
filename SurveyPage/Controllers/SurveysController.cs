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
            SurveyViewModel surveyViewModel = new SurveyViewModel();            
            surveyViewModel.SurveyQuestions.Add(new Question());
            return View(surveyViewModel);
        }

        //POST: Surveys/Create
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SurveyViewModel questions)
        {
            Survey survey = new Survey();
            survey.SurveyName = questions.SurveyName;
            survey.Questions = new List<Question>();
            foreach (var item in questions.SurveyQuestions)
            {
                survey.Questions.Add(item);
            }
            db.Surveys.Add(survey);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Surveys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Survey survey = db.Surveys.Find(id);
            //if (survey == null)
            //{
            //    return HttpNotFound();
            //}

            SurveyViewModel surveyViewModel = new SurveyViewModel();
            surveyViewModel = db.Surveys
                                .Where(x=>x.Id == id)
                                .Select( x=> new SurveyViewModel
                                {
                                    SurveyName = x.SurveyName,
                                    SurveyId = x.Id,
                                    SurveyQuestions = x.Questions
                                }).FirstOrDefault();

            //viewModel = db.UserProfiles
            // .Where(x => x.UserId == id)
            // .Select(x => new EditAdminModelVM
            // {
            //     FirstName = x.FirstName,
            //     LastName = x.LastName,
            //     Email = x.Email,
            //     UserName = x.UserName
            // });


            return View(surveyViewModel);
        }

        // POST: Surveys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit(Survey survey)
        public ActionResult Edit(Survey survey, SurveyViewModel surveyViewModel)
        {
            survey.SurveyName = surveyViewModel.SurveyName;
            survey.Questions.AddRange(surveyViewModel.SurveyQuestions);
            if (ModelState.IsValid)
            {
                db.Entry(survey).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //return View(survey);
            return RedirectToAction("Index");

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
