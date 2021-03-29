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
    public class CompanyController : Controller
    {
        // GET: Company
        public ActionResult Index()
        {
            return View();
        }
        Company CurrentCompany = new Company();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.dtCat = DomainLogic.SelectALL();
            if (Session["Company"] == null)
            {
                filterContext.Result = RedirectToAction("CompanyLogin", "Access");
                return;
            }
            CurrentCompany = (Company)Session["Company"];
            ViewBag.CurrentCompany = CurrentCompany;

            base.OnActionExecuting(filterContext);
        }

        public ActionResult CompanyHome()
        {
            return View(CurrentCompany);
        }

        public ActionResult MyProfile()
        {
            DataTable dtDomain = DomainLogic.SelectALL();
            ViewBag.dtDomain = dtDomain;
            return View(CurrentCompany.CompanyID);
        }

        [HttpPost]
        public ActionResult MyProfileSubmit()
        {
            CurrentCompany.Name = Request.Params["Name"];
            CurrentCompany.Email = Request.Params["Email"];
            CurrentCompany.Mobile = Request.Params["Mobile"];
            CurrentCompany.Username = Request.Params["Username"];
            CurrentCompany.Password = Request.Params["Password"];
            CurrentCompany.IsActive = Request.Params["IsActive"] == "1";
            CurrentCompany.Address = Request.Params["Address"];
            CurrentCompany.DomainID = Convert.ToInt32(Request.Params["DomainID"]);
            CurrentCompany.Status = "PENDING";

            if (Request.Files["CompanyDocs"].ContentLength > 0)
            {
                string filename = DateTime.Now.Ticks.ToString() + "_" + Request.Files["CompanyDocs"].FileName;
                string PhysicalFileName = Server.MapPath("~/CompanyDocs/" + filename);
                Request.Files["CompanyDocs"].SaveAs(PhysicalFileName);
                CurrentCompany.CompanyDocs = filename;
            }
            else
            {
                CurrentCompany.CompanyDocs = "";
            }


            CompanyLogic.Update(CurrentCompany);
            Session["Company"] = CurrentCompany;

            return RedirectToAction("CompanyHome",CurrentCompany.CompanyID);
        }

        public ActionResult VacancyNew()
        {
            return View(CurrentCompany.CompanyID);
        }

        [HttpPost]
        public ActionResult VacancyNewSubmit()
        {
            Vacancy v = new Vacancy();
            v.CompanyID = CurrentCompany.CompanyID;
            v.MinMarks = Convert.ToInt32(Request.Params["MinMarks"]);
            v.Requirements = Request.Params["Requirements"];
            v.ContractDocs = Request.Params["ContractDocs"];
            v.MinExperience =Convert.ToInt32( Request.Params["MinExperience"]);
            v.Status = "PENDING";
            v.StaffID = 0;
           

            VacancyLogic.Insert(v);

            return RedirectToAction("MyVacancyList",CurrentCompany.CompanyID);
        }

        public ActionResult MyVacancyList()
        {
            DataTable dt = VacancyLogic.SelectByCompanyID(CurrentCompany.CompanyID);

            return View(dt);
            
        }

        public ActionResult VacancyEdit()
        {
            Vacancy v = VacancyLogic.SelectByPK(Convert.ToInt32(Request.Params["SID"]));
            DataTable dt = VacancySkillLogic.SelectByVacancyID(Convert.ToInt32(Request.Params["VacancyID"]));
            DataTable dtSkill = SkillLogic.SelectALL();
            ViewBag.dtSkill = dtSkill;
            

            return View(v);

        }

        [HttpPost]
        public ActionResult VacancyEditSubmit()
        {
            
            Vacancy v = VacancyLogic.SelectByPK(Convert.ToInt32(Request.Params["VacancyID"]));
            v.CompanyID = Convert.ToInt32(Request.Params["CompanyID"]);
            v.MinMarks = Convert.ToInt32(Request.Params["MinMarks"]);
            v.Requirements = Request.Params["Requirements"];
            v.ContractDocs = Request.Params["ContractDocs"];
            v.MinExperience = Convert.ToInt32(Request.Params["MinExperience"]);
           // v.Status = "PENDING";
            v.StaffID = Convert.ToInt32(Request.Params["StaffID"]);


            //int StaffID = Convert.ToInt32(Request.Params["StaffID"]);
            //DataTable dtCat = StaffLogic.SelectALL();
            //ViewBag.dtCat = StaffLogic.SelectByPK(StaffID).StaffType;




            VacancyLogic.Update(v);

            //VacancySkill v1 = VacancySkillLogic.SelectByPK(Convert.ToInt32(Request.Params["VacancySkilID"]));
            //v1.VacancyID = Convert.ToInt32(Request.Params["VacancyID"]);
            //v1.SkillID = Convert.ToInt32(Request.Params["SkillID"]);
            //VacancySkillLogic.Insert(v1);

            return RedirectToAction("MyVacancyList");
        }

        [HttpPost] 
        public ActionResult VacancySkillAdd()
        { 
           VacancySkill v1 = new VacancySkill();
            v1.VacancyID = Convert.ToInt32(Request.Params["VacancyID"]);
            v1.SkillID = Convert.ToInt32(Request.Params["SkillID"]);
          VacancySkillLogic.Insert(v1);

            return RedirectToAction("MyVacancyList");

        }
        public ActionResult VacancyDelete()
        {

            VacancyLogic.Delete(Convert.ToInt32(Request.Params["SID"]));
            return RedirectToAction("MyVacancyList");
        }

    }
}
