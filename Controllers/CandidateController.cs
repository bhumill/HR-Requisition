using HRRequisition.Business_Logic;
using HRRequisition.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace HRRequisition.Controllers
{
    public class CandidateController : Controller
    {

        Candidate CurrentCandidate = new Candidate();
        private object tq;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.dtCat = DomainLogic.SelectALL();
            if (Session["Candidate"] == null)
            {
                filterContext.Result = RedirectToAction("CandidateLogin", "Access");
                return;
            }
            CurrentCandidate = (Candidate)Session["Candidate"];
            ViewBag.CurrentCandidate = CurrentCandidate;

            base.OnActionExecuting(filterContext);
        }

        public ActionResult CandidateHome()
        {
          
            return View(CurrentCandidate);
        }
        public ActionResult MyProfile()
        {
            DataTable dtDomain = DomainLogic.SelectALL();
            ViewBag.dtDomain = dtDomain;
            return View(CurrentCandidate.CandidateID);
        }

        [HttpPost]
        public ActionResult MyProfileSubmit()
        {
            CurrentCandidate.Name = Request.Params["Name"];
            CurrentCandidate.Email = Request.Params["Email"];
            CurrentCandidate.Mobile = Request.Params["Mobile"];
            CurrentCandidate.Username = Request.Params["Username"];
            CurrentCandidate.Password = Request.Params["Password"];
            CurrentCandidate.IsActive = Request.Params["IsActive"] == "1";

            CurrentCandidate.Address = Request.Params["Address"];
            CurrentCandidate.City = Request.Params["City"];
            CurrentCandidate.DomainID = Convert.ToInt32(Request.Params["DomainID"]);
            //CurrentCandidate.ResumeFile = Request.Params["ResumeFile"];
            if (Request.Files["ResumeFile"].ContentLength > 0)
            {
                string filename = DateTime.Now.Ticks.ToString() + "_" + Request.Files["ResumeFile"].FileName;
                string PhysicalFileName = Server.MapPath("~/ResumeFile/" + filename);
                Request.Files["ResumeFile"].SaveAs(PhysicalFileName);
                CurrentCandidate.ResumeFile = filename;
            }
            else
            {
                CurrentCandidate.ResumeFile = "";
            }
            CurrentCandidate.Remarks = Request.Params["Remarks"];

            if (Request.Files["ProfilePicture"].ContentLength > 0)
            {
                string filename = DateTime.Now.Ticks.ToString() + "_" + Request.Files["ProfilePicture"].FileName;
                string PhysicalFileName = Server.MapPath("~/ProfilePicture/" + filename);
                Request.Files["ProfilePicture"].SaveAs(PhysicalFileName);
                CurrentCandidate.ProfilePicture = filename;
            }
            else
            {
                CurrentCandidate.ProfilePicture = "";
            }
            // CurrentCandidate.RegisterDate =DateTime.Now(Request.Params["RegisterDate"]);
            CurrentCandidate.Experience = Convert.ToInt32(Request.Params["Experience"]);



            CandidateLogic.Update(CurrentCandidate);
            Session["Candidate"] = CurrentCandidate;

            return RedirectToAction("CandidateHome", CurrentCandidate.CandidateID);
        }

        public ActionResult CandidateSkills()
        {
            DataTable dt = CandidateSkillLogic.SelectByCandidateID(CurrentCandidate.CandidateID);
            // DataTable dt = CandidateSkillLogic.SelectBySkillID(Convert.ToInt32(Request.Params["SkillID"]));
            DataTable dtSkill = SkillLogic.SelectALL();
            ViewBag.dtSkill = dtSkill;
            return View(dt);

        }

        [HttpPost]
        public ActionResult CandidateSkillsSubmit()
        {
            CandidateSkill cs = new CandidateSkill();
            cs.CandidateID = CurrentCandidate.CandidateID;
            cs.SkillID = Convert.ToInt32(Request.Params["SkillID"]);
            cs.Remarks = Request.Params["Remarks"];

            CandidateSkillLogic.Insert(cs);

            return RedirectToAction("CandidateSkills", CurrentCandidate.CandidateID);
        }
        public ActionResult CandidateSkillsList()
        {
            DataTable dt = CandidateSkillLogic.SelectALL();
            return View(dt);
        }
        public ActionResult CandidateSkillsDelete()
        {

            CandidateSkillLogic.Delete(Convert.ToInt32(Request.Params["CandidateSkillID"]));
            return RedirectToAction("CandidateSkills");
        }

        public ActionResult TestStart()
        {


            DataTable dt = CandidateSkillLogic.SelectByCandidateID(CurrentCandidate.CandidateID);
            // DataTable dt = CandidateSkillLogic.SelectBySkillID(Convert.ToInt32(Request.Params["SkillID"]));
            DataTable dtSkill = SkillLogic.SelectALL();
            ViewBag.dtSkill = dtSkill;
            return View(dt);
        }

        public ActionResult TestGenerate()
        {
            DataTable dtCanSkill = CandidateSkillLogic.SelectByCandidateID(CurrentCandidate.CandidateID);

            Test t = new Test();
            t.CandidateID = CurrentCandidate.CandidateID;
            t.StartDatetime = DateTime.Now;
            t.CompletionDateTime = t.StartDatetime;
            t.Status = "ONGOING";
            t.TotalMarks = 0;
            t.ObtainedMarks = 0;
            t.TestID = TestLogic.Insert(t);

            for (int i = 0; i < dtCanSkill.Rows.Count; i++)
            {
                DataTable dtQ = QuestionLogic.SelectBySkillID(Convert.ToInt32(dtCanSkill.Rows[i]["SkillID"]));
                if (dtQ.Rows.Count >= 5)
                {
                    t.TotalMarks += 5;
                    for (int j = 0; j < 5; j++)
                    {
                        TestQuestion tq = new Models.TestQuestion();
                        tq.Answer = 0;
                        tq.IsCorrect = 0;
                        tq.QuestionID = Convert.ToInt32(dtQ.Rows[j]["QuestionID"]);
                        tq.TestID = t.TestID;
                        TestQuestionLogic.Insert(tq);
                    }
                }
            }

            TestLogic.Update(t);

            return RedirectToAction("TestQuestion", new { no = 1 });
        }
        public ActionResult TestQuestion()
        {
            Test t1 = TestLogic.SelectOngoingByCandidateID(CurrentCandidate.CandidateID);
            DataTable dtTQ = TestQuestionLogic.SelectByTestID(t1.TestID);

            int no = Convert.ToInt32(Request.Params["no"]);

            if (dtTQ.Rows.Count >= no)
            {
                TestQuestion tq = TestQuestionLogic.SelectByPK(Convert.ToInt32(dtTQ.Rows[no - 1]["TestQuestionID"]));
                Question q = QuestionLogic.SelectByPK(tq.QuestionID);

                ViewBag.q = q;
                ViewBag.tq = tq;
                ViewBag.no = no + 1;

                return View();
            }
            else
            {

                return RedirectToAction("TestComplete", new { TestID = t1.TestID });
            }

        }
        public ActionResult TestQuestionSubmit()
        {
            Test t1 = TestLogic.SelectOngoingByCandidateID(CurrentCandidate.CandidateID);
            int no = Convert.ToInt32(Request.Params["no"]);

            TestQuestion tq = TestQuestionLogic.SelectByPK(Convert.ToInt32(Request.Params["TestQuestionID"]));
            Question q = QuestionLogic.SelectByPK(tq.QuestionID);

            tq.Answer = Convert.ToInt32(Request.Params["Answer"]);
            if(tq.Answer == q.CorrectAnswer)
            {
                tq.IsCorrect = 1 ;
            }
            TestQuestionLogic.Update(tq);


            return RedirectToAction("TestQuestion", new { no = no });

        }

        public ActionResult TestComplete()
        {
            
            Test t1 = TestLogic.SelectOngoingByCandidateID(CurrentCandidate.CandidateID);
                 t1 = TestLogic.SelectByPK(Convert.ToInt32(Request.Params["TestID"]));
            if (t1.Status == "ONGOING")
            {
                t1.Status = "CLOSED";
                TestLogic.Update(t1);
            }
            return View(t1);
        }

    }
}