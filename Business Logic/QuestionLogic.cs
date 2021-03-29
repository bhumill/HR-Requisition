using HRRequisition.DataAccess;
using HRRequisition.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HRRequisition.Business_Logic
{
    public class QuestionLogic
    {
        public static int Insert(Question q)
        {

            string query = "INSERT INTO Question VALUES(@Title, @Option1, @Option2, @Option3, @Option4, @SkillID, @CorrectAnswer)";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Title", q.Title));
            parameters.Add(new SqlParameter("@Option1", q.Option1));
            parameters.Add(new SqlParameter("@Option2", q.Option2));
            parameters.Add(new SqlParameter("@Option3", q.Option3));
            parameters.Add(new SqlParameter("@Option4", q.Option4));
            parameters.Add(new SqlParameter("@SkillID", q.SkillID));
            parameters.Add(new SqlParameter("@CorrectAnswer", q.CorrectAnswer));


            return DBHelper.ModifyData(query, parameters);
        }

        public static int Update(Question q)
        {
            string query = "UPDATE Question SET Title=@Title, Option1=@Option1,  Option2= @Option2, Option3=@Option3, Option4=@Option4,  CorrectAnswer=@CorrectAnswer  WHERE QuestionID = @QuestionID"; //Dont forget to add SkillID
            List<SqlParameter> parameters = new List<SqlParameter>();
            
            parameters.Add(new SqlParameter("@Title", q.Title));
            parameters.Add(new SqlParameter("@Option1", q.Option1));
            parameters.Add(new SqlParameter("@Option2", q.Option2));
            parameters.Add(new SqlParameter("@Option3", q.Option3));
            parameters.Add(new SqlParameter("@Option4", q.Option4));
           // parameters.Add(new SqlParameter("@SkillID", q.SkillID));
            parameters.Add(new SqlParameter("@CorrectAnswer", q.CorrectAnswer));
            parameters.Add(new SqlParameter("@QuestionID", q.QuestionID));


            return DBHelper.ModifyData(query, parameters);
        }

        internal static DataTable SelectBySkillID(int v, object skillID)
        {
            throw new NotImplementedException();
        }

        public static int Delete(int ID)
        {
            string query = "DELETE Question WHERE QuestionID = @ID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", ID));
            return DBHelper.ModifyData(query, parameters);
        }

        public static Question SelectByPK(int ID)
        {
            string query = "SELECT * FROM Question WHERE QuestionID = @ID";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", ID));
            DataTable dt = DBHelper.SelectData(query, parameters);
            Question q = new Question();
            if (dt.Rows.Count > 0)
            {
                q.QuestionID = ID;
                q.Title = (dt.Rows[0]["Title"].ToString());
                q.Option1 = (dt.Rows[0]["Option1"].ToString());
                q.Option2 =(dt.Rows[0]["Option2"].ToString());
                q.Option3 =(dt.Rows[0]["Option3"].ToString());
                q.Option4 = (dt.Rows[0]["Option4"].ToString());
                q.SkillID = Convert.ToInt32(dt.Rows[0]["SkillID"].ToString());
                q.CorrectAnswer = Convert.ToInt32(dt.Rows[0]["CorrectAnswer"].ToString());



            }
            return q;
        }
        public static DataTable SelectALL()
        {
            string query = "SELECT * FROM Question INNER JOIN Skill ON Skill.SkillID=Question.SkillID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            return DBHelper.SelectData(query, parameters);
        }

        public static DataTable SelectBySkillID(int SkillID)
        {
            string query = @"SELECT * FROM Question INNER JOIN Skill ON Skill.SkillID=Question.SkillID WHERE Question.SkillID = @SkillID";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SkillID", SkillID));
            return DBHelper.SelectData(query, parameters);
        }
    }
}