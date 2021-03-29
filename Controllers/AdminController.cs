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
    public class AdminController : Controller
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
            if (CurrentStaff.StaffType != "ADMIN")
            {
                filterContext.Result = RedirectToAction("StaffLogin", "Access");
                return;
            }

            //CurrentStaff = (Staff)Session["Staff"];
            //if (CurrentStaff.StaffType != "HR Manager")
            //{
            //    filterContext.Result = RedirectToAction("StaffLogin", "Access");
            //    return;
            //}

            //CurrentStaff = (Staff)Session["Staff"];
            //if (CurrentStaff.StaffType != "Placement Manager")
            //{
            //    filterContext.Result = RedirectToAction("StaffLogin", "Access");
            //    return;
            //}

            ViewBag.CurrentStaff = CurrentStaff;

            base.OnActionExecuting(filterContext);
        }

        // GET: Admin
        public ActionResult StaffNew()
        {
            DataTable dtDomain = DomainLogic.SelectALL();
            ViewBag.dtDomain = dtDomain;
            return View();

            
        }

        [HttpPost]
        public ActionResult StaffNewSubmit()
        {
            Staff s = new Staff();
            s.Name = Request.Params["Name"];
            s.Email = Request.Params["Email"];
            s.Mobile = Request.Params["Mobile"];
            s.Username = Request.Params["Username"];
            s.Password = Request.Params["Password"];
            s.IsActive = Request.Params["IsActive"] == "1";
            s.StaffType = Request.Params["StaffType"];
            s.DomainID = Convert.ToInt32(Request.Params["DomainID"]);



            StaffLogic.Insert(s);

            return RedirectToAction("StaffList");
        }

        public ActionResult StaffList()
        {
            DataTable dt = StaffLogic.SelectALL();
           
            return View(dt);
        }

        public ActionResult StaffEdit()
        {
                Staff s = StaffLogic.SelectByPK(Convert.ToInt32(Request.Params["SID"]));
            //DataTable dtStaff = StaffLogic.SelectALL();
            //ViewBag.dtStaff = dtStaff;
            return View(s);
            
        }

        [HttpPost]
        public ActionResult StaffEditSubmit()
        {
            Staff s = StaffLogic.SelectByPK(Convert.ToInt32(Request.Params["StaffID"]));
            s.Name = Request.Params["Name"];
            s.Email = Request.Params["Email"];
            s.Mobile = Request.Params["Mobile"];
            s.Username = Request.Params["Username"];
            s.Password = Request.Params["Password"];
            s.IsActive = Request.Params["IsActive"] == "1";
            s.StaffType = Request.Params["StaffType"];


          



            StaffLogic.Update(s);

            return RedirectToAction("StaffList");
        }

        public ActionResult DomainNew()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DomainNewSubmit()
        {
            Domain d = new Domain();
            d.DomainName = Request.Params["DomainName"];


            DomainLogic.Insert(d);
            return RedirectToAction("DomainList");
        }

        public ActionResult DomainList()
        {
            DataTable dt = DomainLogic.SelectALL();
            return View(dt);
        }

        public ActionResult DomainEdit()
        {
           // int DomainID = Convert.ToInt32(Request.Params["DomainID"]);
             //Domain d = DomainLogic.SelectByPK(DomainID);

           // return View(d);
            Domain d = DomainLogic.SelectByPK(Convert.ToInt32(Request.Params["SID"]));
            return View(d);
        }

        [HttpPost]
        public ActionResult DomainEditSubmit()
        {
            Domain d = DomainLogic.SelectByPK(Convert.ToInt32(Request.Params["DomainID"]));
            d.DomainName = Request.Params["DomainName"];


            DomainLogic.Update(d);
            return RedirectToAction("DomainList");
        }

        public ActionResult SkillNew()
        {
            DataTable dtDomain = DomainLogic.SelectALL();
            ViewBag.dtDomain = dtDomain;
            return View();
        }
        [HttpPost]
        public ActionResult SkillNewSubmit()
        {
            Skill s = new Skill();
            s.DomainID = Convert.ToInt32(Request.Params["DomainID"]);
            s.SkillName = Request.Params["SkillName"];


            SkillLogic.Insert(s);
            return RedirectToAction("SkillList");
        }

        public ActionResult SkillList()
        {
            DataTable dt = SkillLogic.SelectALL();
           
            return View(dt);
        }

        public ActionResult SkillEdit()
        {
            
            Skill s = SkillLogic.SelectByPK(Convert.ToInt32(Request.Params["SID"]));

            return View(s);
        }

        [HttpPost]
        public ActionResult SkillEditSubmit()
        {
            Skill s = SkillLogic.SelectByPK(Convert.ToInt32(Request.Params["SkillID"]));
            s.SkillName = Request.Params["SkillName"];


            SkillLogic.Update(s);
            return RedirectToAction("SkillList");
        }

        public ActionResult CompanyListPending()
        {
            DataTable dt = CompanyLogic.SelectByStatus("PENDING");

            return View(dt);
            
        }
        public ActionResult CompanyListApproved()
        {
            DataTable dt = CompanyLogic.SelectByStatus("APPROVED");

            return View(dt);

        }

        public ActionResult CompanyDetails()
        {


            Company c = CompanyLogic.SelectByPK(Convert.ToInt32(Request.Params["SID"]));
            return View(c);
        }

        [HttpPost]
        public ActionResult CompanyDetailsSubmit()
        {

            Company c = CompanyLogic.SelectByPK(Convert.ToInt32(Request.Params["CompanyID"]));
            c.CompanyID = Convert.ToInt32(Request.Params["CompanyID"]);
            c.Name = Request.Params["Name"];
            c.Email = Request.Params["Email"];
            c.Mobile = Request.Params["Mobile"];
            c.Username = Request.Params["Username"];
          //  c.Password = ;
            c.IsActive = Request.Params["IsActive"] == "1";
            c.Address = Request.Params["Address"];
            c.DomainID = Convert.ToInt32(Request.Params["DomainID"]); 
            c.Status = Request.Params["Status"];
            c.CompanyDocs = Request.Params["CompanyDocs"];
           


            //int StaffID = Convert.ToInt32(Request.Params["StaffID"]);
            //DataTable dtCat = StaffLogic.SelectALL();
            //ViewBag.dtCat = StaffLogic.SelectByPK(StaffID).StaffType;



            CompanyLogic.Update(c);
            return RedirectToAction("CompanyListPending");

        }

    }
} 