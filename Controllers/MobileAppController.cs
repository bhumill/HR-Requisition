using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRRequisition.Models;
using HRRequisition.Business_Logic;
using System.Data;
using System.Web.Script.Serialization;


namespace HRRequisition.Controllers
{
    public class MobileAppController : Controller
    {
        public static string DataTabletoJSON(DataTable dt)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, object> row = null;
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.DataType == typeof(DateTime))
                    {
                        row.Add(dc.ColumnName, dr[dc] != DBNull.Value ? Convert.ToDateTime(dr[dc]).ToString("yyyy-MM-dd#HH:mm") : "");
                    }
                    else
                    {
                        row.Add(dc.ColumnName, dr[dc] != DBNull.Value ? dr[dc] : "");
                    }
                }
                rows.Add(row);
            }
            return jss.Serialize(rows);
        }

        public ActionResult SignIn()
        {
            Candidate c = CandidateLogic.SelectByUnPw(Request.Params["Username"], Request.Params["Password"]);
            if (c.CandidateID > 0)
            {
                return Content("success");
            }
            else
            {
                return Content("fail");
            }
        }

        public ActionResult GetAllDomains()
        {
            DataTable dt = DomainLogic.SelectALL();
            string json = DataTabletoJSON(dt);
            return Content(json);
        }

        public ActionResult GetTopicsByDomainID()
        {
            DataTable dt = TopicLogic.SelectByDomainID(Convert.ToInt32(Request.Params["DomainID"]));
            string json = DataTabletoJSON(dt);
            return Content(json);
        }

        public ActionResult GetCommentsByTopicID()
        {
            DataTable dt = CommentLogic.SelectByTopicID(Convert.ToInt32(Request.Params["TopicID"]));
            string json = DataTabletoJSON(dt);
            return Content(json);
        }

        public ActionResult NewTopic()
        {
            Candidate c = CandidateLogic.SelectByUnPw(Request.Params["Username"], Request.Params["Password"]);
            if (c.CandidateID > 0)
            {
                try
                {
                    Topic t = new Topic();
                    t.CandidateID = c.CandidateID;
                    t.Description = Request.Params["Description"];
                    t.DomainID = Convert.ToInt32(Request.Params["DomainID"]);
                    t.StartDate = DateTime.Now;
                    t.Title = Request.Params["Title"];
                    TopicLogic.Insert(t);

                    return Content("success");
                }
                catch { }
            }
            return Content("fail");
        }

        public ActionResult NewComment()
        {
            Candidate c = CandidateLogic.SelectByUnPw(Request.Params["Username"], Request.Params["Password"]);
            if (c.CandidateID > 0)
            {
                try
                {
                    Comment co = new Models.Comment();
                    co.CandidateID = c.CandidateID;
                    co.CommentDate = DateTime.Now;
                    co.CommentText= Request.Params["CommentText"];
                    co.TopicID= Convert.ToInt32(Request.Params["TopicID"]);
                    CommentLogic.Insert(co);

                    return Content("success");
                }
                catch { }
            }
            return Content("fail");
        }
    }
}