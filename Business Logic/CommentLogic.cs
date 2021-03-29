using HRRequisition.DataAccess;
using HRRequisition.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HRRequisition.Business_Logic
{
    public class CommentLogic
    {
        public static int Insert(Comment c)
        {

            string query = "INSERT INTO Comment VALUES(@CommentText, @CandidateID, @CommentDate, @TopicID)";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CandidateID", c.CommentText));
            parameters.Add(new SqlParameter("@SkillID", c.CandidateID));
            parameters.Add(new SqlParameter("@CommentDate", c.CommentDate));
            parameters.Add(new SqlParameter("@TopicID", c.TopicID));


            return DBHelper.ModifyData(query, parameters);
        }

        public static int Update(Comment c)
        {
            string query = "UPDATE Comment SET CommentText=@CommentText CandidateID=@CandidateID  CommentDate= @CommentDate TopicID=@TopicID  WHERE CommentID = @CommentID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CommentText", c.CommentText));
            parameters.Add(new SqlParameter("@CandidateID", c.CandidateID));
            parameters.Add(new SqlParameter("@CommentDate", c.CommentDate));
            parameters.Add(new SqlParameter("@TopicID", c.TopicID));
            parameters.Add(new SqlParameter("@CommentID", c.CommentID));

            return DBHelper.ModifyData(query, parameters);
        }

        public static int Delete(int ID)
        {
            string query = "DELETE Comment WHERE CommentID = @ID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", ID));
            return DBHelper.ModifyData(query, parameters);
        }

        public static Comment SelectByPK(int ID)
        {
            string query = "SELECT * FROM Comment WHERE CommentID = @ID";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", ID));
            DataTable dt = DBHelper.SelectData(query, parameters);
            Comment c = new Comment();
            if (dt.Rows.Count > 0)
            {
                c.CommentID = ID;
                c.CommentText = (dt.Rows[0]["CommentText"].ToString());
                c.CandidateID = Convert.ToInt32(dt.Rows[0]["CandidateID"].ToString());
                c.CommentDate = Convert.ToDateTime(dt.Rows[0]["CommentDate"].ToString());
                c.TopicID = Convert.ToInt32(dt.Rows[0]["TopicID"].ToString());



            }
            return c;
        }

        public static DataTable SelectALL()
        {
            string query = "SELECT * FROM Comment";
            List<SqlParameter> parameters = new List<SqlParameter>();
            return DBHelper.SelectData(query, parameters);
        }

        public static DataTable SelectByTopicID(int TopicID)
        {
            string query = "SELECT * FROM Comment WHERE TopicID = @TopicID";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@TopicID", TopicID));
            return DBHelper.SelectData(query, parameters);
        }
    }
}