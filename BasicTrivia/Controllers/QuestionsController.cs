using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BasicTrivia.Models;

namespace BasicTrivia.Controllers
{
    public class QuestionsController : Controller
    {
        private QuestionDBContext db = new QuestionDBContext();

        // Trivia Home Page
        public ActionResult Index()
        {
            return View();
        }
        
        
        // GET: Questions
        public ActionResult ListQuestions()
        {
            return View(db.Questions.ToList());
        }

        // GET: Questions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: Questions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Trivia,Answer,Choice1,Choice2,Choice3,Choice4,Choice5")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(question);
        }

        // GET: Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Trivia,Answer,Choice1,Choice2,Choice3,Choice4,Choice5")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(question);
        }

        // GET: Questions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
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


        // Start Controller Methods to execute game logic

        public ActionResult NewGame()
        {
            var totalQuestions = db.Questions.Count();
            List<int> notAsked = Enumerable.Range(1, totalQuestions).ToList();
            int right = 0;
            int wrong = 0;

            return ShowNextQuestion(notAsked, right, wrong);
          
        }

        public ActionResult ShowNextQuestion(List<int> notAsked, int right, int wrong)
        {
            //check if no questions remaining
            if (notAsked == null)
            {
                return EndGame(right, wrong);
            }
            
            // Randomize Question asked and remove from notAsked array
            Random rnd = new Random();
            int nextQuestion = rnd.Next(0, notAsked.Count());
            

            //Get Question from db
            int index = notAsked[nextQuestion];
            Question question = db.Questions.SingleOrDefault(q => q.ID == index);
            var questionText = question.Trivia;

            //Arrange choices randomly
            var rndArray = Enumerable.Range(1, 4).OrderBy(t => rnd.Next()).ToArray();
            var rndAnswer = rnd.Next(1,4);
            foreach (var n in rndArray)
            {
                switch (n)
                {
                    case 1:
                        if (n == rndAnswer)
                        {
                            var option1 = question.Answer;
                        }
                        else
                        {
                            var option1 = GetChoice(n, question);
                        }
                        break;
                    case 2:
                        if (n == rndAnswer)
                        {
                            var option2 = question.Answer;
                        }
                        else
                        {
                            var option2 = GetChoice(n, question);
                        }
                        break;
                    case 3:
                        if (n == rndAnswer)
                        {
                            var option3 = question.Answer;
                        }
                        else
                        {
                            var option3 = GetChoice(n, question);
                        }
                        break;
                    case 4:
                        if (n == rndAnswer)
                        {
                            var option4 = question.Answer;
                        }
                        else
                        {
                            var option4 = GetChoice(n, question);
                        }
                        break;
                
                }  //end switch
            }  //end foreach 

            notAsked.RemoveAt(nextQuestion);
            return View();
        } // End ShowNextQuestion

        
        
        private object GetChoice(int n, Question question)
        {
            var choice = "";
            switch (n)
            {
                case 1:
                    choice = question.Choice1;
                    break;
                case 2:
                    choice = question.Choice2;
                    break;
                case 3:
                    choice = question.Choice3;
                    break;
                case 4:
                    choice = question.Choice4;
                    break;
                case 5:
                    choice = question.Choice5;
                    break;
            }
            return choice;
        }





        public ActionResult EndGame(int right, int wrong)
        {
            return View();
        }



    }
}
