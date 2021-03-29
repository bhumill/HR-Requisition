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
    public class StaffController : Controller
    {
        // GET: Staff
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult StaffHome()
        {
            DataTable dtStaff = StaffLogic.SelectALL();
            ViewBag.dtStaff = dtStaff;
            return View(dtStaff);
        }

        //public ActionResult MyProfile()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult MyProfileSubmit()
        //{
        //    Staff s = StaffLogic.SelectByPK(Convert.ToInt32(Request.Params["StaffID"]));
        //    s.Name = Request.Params["Name"];
        //    s.Email = Request.Params["Email"];
        //    s.Mobile = Request.Params["Mobile"];
        //    s.Username = Request.Params["Username"];
        //    s.Password = Request.Params["Password"];
        //    s.IsActive = Request.Params["IsActive"] == "1";
        //    s.StaffType = Request.Params["StaffType"];


        //    //int StaffID = Convert.ToInt32(Request.Params["StaffID"]);
        //    //DataTable dtCat = StaffLogic.SelectALL();
        //    //ViewBag.dtCat = StaffLogic.SelectByPK(StaffID).StaffType;



        //    StaffLogic.Update(s);
           

        //}
        }
}