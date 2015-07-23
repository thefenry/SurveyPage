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
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            survey.CreatedBy = currentUser;
            //survey.User = currentUser; 
            //survey.Questions = new List<Question>();
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

            SurveyViewModel surveyViewModel = new SurveyViewModel();
            surveyViewModel = db.Surveys
                                .Where(x => x.Id == id)
                                .Select(x => new SurveyViewModel
                                {
                                    SurveyName = x.SurveyName,
                                    SurveyId = x.Id,
                                    SurveyQuestions = x.Questions
                                }).FirstOrDefault();

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
            if (ModelState.IsValid)
            {
                foreach (var question in surveyViewModel.SurveyQuestions)
                {
                    if (true)
                    {
                        var existingQuestion = db.Questions.Where(x => x.Id == question.Id).FirstOrDefault();
                        if (existingQuestion == null)
                        {
                            question.SurveyId = survey.Id;
                            db.Questions.Add(question);
                        }
                        else
                        {
                            existingQuestion.SurveyQuestion = question.SurveyQuestion;
                            survey.Questions.Add(existingQuestion);
                        }
                    }
                }

                db.Entry(survey).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(surveyViewModel);
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

        public ActionResult TakeSurvey(int id)
        {
            SurveyResponseViewModel surveyResponse = new SurveyResponseViewModel();
            surveyResponse = db.Surveys
                .Where(x => x.Id == id)
                .Select(x => new SurveyResponseViewModel
                {
                    SurveyId = x.Id,
                    SurveyName = x.SurveyName,
                    SurveyQuestions = x.Questions
                }).FirstOrDefault();
            foreach (var item in surveyResponse.SurveyQuestions)
            {
                Answer answer = new Answer();
                answer.Question = item;
                answer.QuestionId = item.Id;
                surveyResponse.QuestionAnswers.Add(answer);                
            }
            return View(surveyResponse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TakeSurvey(SurveyResponseViewModel SurveyResponseViewModel)
        {
            foreach (var item in SurveyResponseViewModel.QuestionAnswers)
            {
                var question = db.Questions.Where(x => x.Id == item.QuestionId).FirstOrDefault();
                question.Answers.Add(item);
            }
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
