using HRRequisition.DataAccess;
using HRRequisition.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HRRequisition.Business_Logic
{
    public class TopicLogic
    {
         public static SqlDbType ID { get; private set; }

        public static int Insert(Topic t)
        {
            string query = "INSERT INTO Topic VALUES(@Title, @Description, @CandidateID, @DomainID, @StartDate)";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Title", t.Title));
            parameters.Add(new SqlParameter("@Description", t.Description));
            parameters.Add(new SqlParameter("@CandidateID", t.CandidateID));
            parameters.Add(new SqlParameter("@DomainID", t.DomainID));
            parameters.Add(new SqlParameter("@StartDate", t.StartDate));
            

            return DBHelper.ModifyData(query, parameters);
        }

        public static int Update(Topic t)
        {
            string query = "UPDATE Topic SET Title=@Title, Description = @Description, StartDate = @StartDate WHERE TopicID = @TopicID";  // Don't fprget to add domain ID,staffID CompanyID=@CompanyID,
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Title", t.Title));
            parameters.Add(new SqlParameter("@Description", t.Description));
            //parameters.Add(new SqlParameter("@CandidateID", t.CandidateID));
            //parameters.Add(new SqlParameter("@DomainID", t.DomainID));
            parameters.Add(new SqlParameter("@StartDate", t.StartDate));
            parameters.Add(new SqlParameter("@TopicID", t.TopicID));


            return DBHelper.ModifyData(query, parameters);

        }
        public static int Delete(int ID)
        {
            string query = "DELETE Topic WHERE TopicID = @ID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", ID));
            return DBHelper.ModifyData(query, parameters);
        }

        public static Topic SelectByPK(int TopicID)
        {
            string query = "SELECT * FROM Vacancy WHERE VacancyID = @VacancyID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@TopicID", TopicID));
            DataTable dt = GetDt(query, parameters);

            Topic t = new Topic();
            t.TopicID = TopicID;
            t.Title = (dt.Rows[0]["Title"].ToString());
            t.Description =(dt.Rows[0]["Description"].ToString());
            t.CandidateID = Convert.ToInt32(dt.Rows[0]["CandidateID"].ToString());
            t.DomainID = Convert.ToInt32(dt.Rows[0]["DomainID"].ToString());
            t.StartDate = Convert.ToDateTime(dt.Rows[0]["StartDate"]);
           
            return t;
        }

        private static DataTable GetDt(string query, List<SqlParameter> parameters)
        {
            return DBHelper.SelectData(query, parameters);
        }

        public static DataTable SelectALL()
        {
            string query = "SELECT * FROM Topic";
            List<SqlParameter> parameters = new List<SqlParameter>();
            return DataAccess.DBHelper.SelectData(query, parameters);
        }

        public static DataTable SelectByDomainID(int DomainID)
        {
            string query = "SELECT * FROM Topic WHERE DomainID = @DomainID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@DomainID", DomainID));
            return GetDt(query, parameters);
            
        }
    }
}