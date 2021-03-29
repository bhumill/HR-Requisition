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
    public class HRManagerController : Controller
    {
        Staff CurrentStaff = new Staff();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["Staff"] == null)
            {
                filterContext.Result = RedirectToAction("StaffLogin", "Access");
                return;
            }

            CurrentStaff = (Staff)Session["Staff"];
            if (CurrentStaff.StaffType != "HR Manager")
            {
                filterContext.Result = RedirectToAction("StaffLogin", "Access");
                return;
            }
        }

        public ActionResult QuestionNew()
        {
            DataTable dtSkill = SkillLogic.SelectALL();
            ViewBag.dtSkill = dtSkill;
            return View();
        }

        [HttpPost]
        public ActionResult QuestionNewSubmit()
        {
            Question q = new Question();
            q.Title = Request.Params["Title"];
            q.Option1 = Request.Params["Option1"];
            q.Option2 = Request.Params["Option2"];
            q.Option3 = Request.Params["Option3"];
            q.Option4 = Request.Params["Option4"];
            q.SkillID = Convert.ToInt32(Request.Params["SkillID"]);
            q.CorrectAnswer = Convert.ToInt32(Request.Params["CorrectAnswer"]);

            QuestionLogic.Insert(q);
            DataTable dtSkill = SkillLogic.SelectALL();
            ViewBag.dtSkill = dtSkill;

            return RedirectToAction("QuestionList");
        }

        public ActionResult QuestionList()
        {
            DataTable dt = QuestionLogic.SelectALL();

            return View(dt);
        }

        public ActionResult QuestionEdit()
        {
            Question q = QuestionLogic.SelectByPK(Convert.ToInt32(Request.Params["SID"]));
            DataTable dtSkill = SkillLogic.SelectALL();
            ViewBag.dtSkill = dtSkill;
            return View(q);

        }

        [HttpPost]
        public ActionResult QuestionEditSubmit()
        {
            Question q = QuestionLogic.SelectByPK(Convert.ToInt32(Request.Params["QuestionID"]));
            q.Title = Request.Params["Title"];
            q.Option1 = Request.Params["Option1"];
            q.Option2 = Request.Params["Option2"];
            q.Option3 = Request.Params["Option3"];
            q.Option4 = Request.Params["Option4"];
           // q.SkillID = Convert.ToInt32(Request.Params["SkillID"]); 
            q.CorrectAnswer = Convert.ToInt32(Request.Params["CorrectAnswer"]);



            QuestionLogic.Update(q);

            return RedirectToAction("QuestionList");
        }

        public ActionResult CandidateSearch()
        {
            DataTable dt = CandidateLogic.SelectALL();
            return View(dt);

        }

        public ActionResult CandidateEdit()
        {
            DataTable dtCandidate = CandidateLogic.SelectALL();
            ViewBag.dtCandidate = dtCandidate;
            Candidate c = CandidateLogic.SelectByPK(Convert.ToInt32(Request.Params["CID"]));
            c.Name = Request.Params["Name"];
            c.Email = Request.Params["Email"];
            c.Mobile = Request.Params["Mobile"];
            c.Username = Request.Params["Username"];
            c.IsActive = Request.Params["IsActive"]== "1";
            c.Address = (Request.Params["Address"]);
            c.City = (Request.Params["City"]);

            CandidateLogic.Update(c);

            return View(dtCandidate);


        }
    }
} 