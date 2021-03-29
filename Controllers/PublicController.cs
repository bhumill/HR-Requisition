using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRRequisition.Business_Logic;
using HRRequisition.Models;
using System.Data;

namespace HRRequisition.Controllers
{
    public class PublicController : Controller
    {
        // GET: Public
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StaffLogin()
        {
            Session["Staff"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult StaffLoginSubmit()
        {
            Staff s = StaffLogic.SelectByUnPw(Request.Params["Username"], Request.Params["Password"]);
            if (s.StaffID > 0)
            {
                Session["Staff"] = s;
                if (s.StaffType == "ADMIN")
                {
                    return RedirectToAction("Index", "ADMIN");
                }
                else if (s.StaffType == "GENERAL MANAGER")
                {
                    return RedirectToAction("Index", "GeneralManager");
                }
                else
                {
                    return RedirectToAction("StaffLogin");
                }
            }
            else
            {
                return RedirectToAction("StaffLogin");
            }
        }

       
    }
}