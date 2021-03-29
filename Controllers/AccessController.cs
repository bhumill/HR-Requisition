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
    public class AccessController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.dtCat = DomainLogic.SelectALL();
            ViewBag.CurrentCustomer = Session["Candidate"];

            base.OnActionExecuting(filterContext);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StaffLogin()
        {
            Session["Staff"] = null;
            Session["Company"] = null;
            Session["Candidate"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult StaffLoginSubmit()
        {
            Staff s = StaffLogic.SelectByUnPw(Request.Params["Username"], Request.Params["Password"]);
            if (s.StaffID > 0)
            {
                Session["Staff"] = s;
                    return RedirectToAction("StaffHome", "Staff");
                
            }
            else
            {
                return RedirectToAction("StaffLogin");
            }
        }

        public ActionResult CandidateRegistration()
        {
            Session["Staff"] = null;
            Session["Company"] = null;
            Session["Candidate"] = null;
            return View();
        }


        [HttpPost]
        public ActionResult CandidateRegistrationSubmit()
        {
            Candidate c = new Models.Candidate();
            c.Name = Request.Params["Name"];
            c.Email = Request.Params["Email"];
            c.Mobile = Request.Params["Mobile"];
            c.Username = Request.Params["Username"];
            c.Password = Request.Params["Password"];
            c.IsActive = true;
            c.Address = "";
            c.City = "";
            c.ResumeFile ="";
            c.Remarks = "";
            c.ProfilePicture = "";
            c.RegisterDate = DateTime.Now;


            CandidateLogic.Insert(c);

            return RedirectToAction("CandidateLogin");
        }

        public ActionResult CandidateLogin()
        {
            Session["Staff"] = null;
            Session["Company"] = null;
            Session["Candidate"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult CandidateLoginSubmit()
        {
            Candidate c = CandidateLogic.SelectByUnPw(Request.Params["Username"], Request.Params["Password"]);
            if (c.CandidateID > 0)
            {
                Session["Candidate"] = c;
                return RedirectToAction("CandidateHome", "Candidate");
            }
            else
            {  
                return RedirectToAction("CandidateLogin");
            }
        }

        public ActionResult CompanyRegistration()
        {
            Session["Staff"] = null;
            Session["Company"] = null;
            Session["Candidate"] = null;
            return View();
        }


        [HttpPost]
        public ActionResult CompanyRegistrationSubmit()
        {
            Company c = new Models.Company();
            c.Name = Request.Params["Name"];
            c.Email = Request.Params["Email"];
            c.Mobile = Request.Params["Mobile"];
            c.Username = Request.Params["Username"];
            c.Password = Request.Params["Password"];
            c.IsActive = true;
            c.Address = "";
            c.Status = "";
            c.CompanyDocs = "";
            


            CompanyLogic.Insert(c);

            return RedirectToAction("CompanyLogin");
        }

        public ActionResult CompanyLogin()
        {
            Session["Staff"] = null;
            Session["Company"] = null;
            Session["Candidate"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult CompanyLoginSubmit()
        {
            Company c = CompanyLogic.SelectByUnPw(Request.Params["Username"], Request.Params["Password"]);
            if (c.CompanyID > 0)
            {
                Session["Company"] = c;
                return RedirectToAction("CompanyHome", "Company");
            }
            else
            {
                return RedirectToAction("CompanyLogin");
            }
        }
    }
}