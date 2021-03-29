using HRRequisition.Business_Logic;
using HRRequisition.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HRRequisition.Controllers
{
    public class PlacementManagerController : Controller
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
            if (CurrentStaff.StaffType != "Placement Manager")
            {
                filterContext.Result = RedirectToAction("StaffLogin", "Access");
                return;
            }
        }
        public ActionResult VacancyListPending()
        {
            DataTable dt = VacancyLogic.SelectByStatus("PENDING");

            return View(dt);
        }

        public ActionResult VacancyListOngoing()
        {
            DataTable dt = VacancyLogic.SelectByStatus("ONGOING");

            return View(dt);
        }
        public ActionResult VacancyListClosed()
        {
            DataTable dt = VacancyLogic.SelectByStatus("CLOSED");

            return View(dt);
        }

        public ActionResult VacancyDetails()
        {
            Vacancy v = VacancyLogic.SelectByPK(Convert.ToInt32(Request.Params["SID"]));
            return View(v);
        }

        [HttpPost]
        public ActionResult VacancyDetailsSubmit()
        {

            Vacancy v = VacancyLogic.SelectByPK(Convert.ToInt32(Request.Params["VacancyID"]));
            v.CompanyID = Convert.ToInt32(Request.Params["CompanyID"]);
            v.MinMarks = Convert.ToInt32(Request.Params["MinMarks"]);
            v.Requirements = Request.Params["Requirements"];
            v.ContractDocs = Request.Params["ContractDocs"];
            v.MinExperience = Convert.ToInt32(Request.Params["MinExperience"]);
            v.Status = Request.Params["Status"];
            v.StaffID = CurrentStaff.StaffID;


            //int StaffID = Convert.ToInt32(Request.Params["StaffID"]);
            //DataTable dtCat = StaffLogic.SelectALL();
            //ViewBag.dtCat = StaffLogic.SelectByPK(StaffID).StaffType;



            VacancyLogic.Update(v);
            return RedirectToAction("VacancyListPending");

        }

        public ActionResult VacancyMatching()
        {
            DataTable dtVacancy = VacancyLogic.SelectALL();
            ViewBag.dtVacancy = dtVacancy;
            

            Vacancy v = VacancyLogic.SelectByPK(Convert.ToInt32(Request.Params["SID"]));
            DataTable dt = CandidateLogic.SearchMatching(Convert.ToInt32(Request.Params["SID"]));
            return View(dt);
        }

        public ActionResult ShareVacancyToCandidate()
        {
            Vacancy v = VacancyLogic.SelectByPK(Convert.ToInt32(Request.Params["VID"]));
            Company co = CompanyLogic.SelectByPK(v.CompanyID);
            Candidate ca = CandidateLogic.SelectByPK(Convert.ToInt32(Request.Params["CID"]));

            var senderEmail = new MailAddress("hiredpvtltd@gmail.com", "Hired");
            var receiverEmail = new MailAddress(ca.Email, ca.Name);
            var password = "hiredpvtltd123";
            var sub = "New Vacancy for you in " + co.Name;
            var body = "Company Name" + co.Name + "Company Email" + co.Email + "Company Mobile" + co.Mobile  +"Company Address" + co.Address + "ContractDetails" + v.ContractDocs + "Requirements" + v.Requirements + "MinMarks" + v.MinMarks + "MinExperience" + v.MinExperience;
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                //DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };
            using (var mess = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = sub,
                Body = body
            })
            {

                smtp.Send(mess);
            }
            return RedirectToAction("VacancyMatching",new {SID= Request.Params["VID"] });

        }

        //[HttpPost]
        //public ActionResult VacancyMatchingInformation1()
        //{
        //    Vacancy v = VacancyLogic.SelectByPK(Convert.ToInt32(Request.Params["SID"]));
        //    DataTable dt = VacancyLogic.SelectByCompanyID(Convert.ToInt32(Request.Params["CompanyID"]));


        //    var senderEmail = new MailAddress("hiredpvtltd@gmail.com", "Hired");
        //    var receiverEmail = new MailAddress(Request.Params["receiver"], "Receiver");
        //    var password = "hiredpvtltd123";
        //    var sub = Request.Params["subject"];
        //    var body = Request.Params["Message " + v + dt];
        //    var smtp = new SmtpClient
        //    {
        //        Host = "smtp.gmail.com",
        //        Port = 587,
        //        EnableSsl = true,
        //        //DeliveryMethod = SmtpDeliveryMethod.Network,
        //        UseDefaultCredentials = false,
        //        Credentials = new NetworkCredential(senderEmail.Address, password)
        //    };
        //    using (var mess = new MailMessage(senderEmail, receiverEmail)
        //    {
        //        Subject = sub,
        //        Body = body
        //    })
        //    {

        //        smtp.Send(mess);
        //    }
        //    return View();

        //}
        public ActionResult ShareCandidateDetailToCompany()
        {
            Vacancy v = VacancyLogic.SelectByPK(Convert.ToInt32(Request.Params["VID"]));
        Company co = CompanyLogic.SelectByPK(v.CompanyID);
        Candidate ca = CandidateLogic.SelectByPK(Convert.ToInt32(Request.Params["CID"]));

        var senderEmail = new MailAddress("hiredpvtltd@gmail.com", "Hired");
        var receiverEmail = new MailAddress(co.Email, co.Name);
        var password = "hiredpvtltd123";
        var sub = "New Candidates for your Vacancy" + ca.Name;
        var body = "Candidate Name" + ca.Name + "Candidate Email" + ca.Email + "Candidate Mobile" + ca.Mobile + "Candidate Address" + ca.Address + "ContractDetails" + v.ContractDocs + "Requirements" + v.Requirements + "Resume of Candidate" + ca.ResumeFile  ;
        var smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            //DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(senderEmail.Address, password)
        };
            using (var mess = new MailMessage(senderEmail, receiverEmail)
{
    Subject = sub,
    Body = body
})
            {

                smtp.Send(mess);
            }
            return RedirectToAction("VacancyMatching",new {SID= Request.Params["VID"] });

        }

    }

}
