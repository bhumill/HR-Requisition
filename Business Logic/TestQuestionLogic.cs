using HRRequisition.DataAccess;
using HRRequisition.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HRRequisition.Business_Logic
{
    public class TestQuestionLogic
    {
        public static int Insert(TestQuestion tq)
        {

            string query = "INSERT INTO TestQuestion VALUES(@TestID, @QuestionID, @Answer, @IsCorrect)";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@TestID", tq.TestID));
            parameters.Add(new SqlParameter("@QuestionID", tq.QuestionID));
            parameters.Add(new SqlParameter("@Answer", tq.Answer));
            parameters.Add(new SqlParameter("@IsCorrect", tq.IsCorrect));
           


            return DBHelper.ModifyData(query, parameters);
        }

        public static int Update(TestQuestion tq)
        {
            string query = "UPDATE TestQuestion SET Answer=@Answer, IsCorrect=@IsCorrect WHERE TestQuestionID = @TestQuestionID"; //Dont forget to add SkillID
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@Answer", tq.Answer));
            parameters.Add(new SqlParameter("@IsCorrect", tq.IsCorrect));
            parameters.Add(new SqlParameter("@TestQuestionID", tq.TestQuestionID));

            int x = DBHelper.ModifyData(query, parameters);


            string query2 = "SELECT * FROM TestQuestion WHERE TestID = @TestID AND IsCorrect = 1";
            List<SqlParameter> parameters2 = new List<SqlParameter>();
            parameters2.Add(new SqlParameter("@TestID", tq.TestID));
            DataTable dt = DBHelper.SelectData(query2, parameters2);

            Test t = TestLogic.SelectByPK(tq.TestID);
            t.ObtainedMarks = dt.Rows.Count;
            TestLogic.Update(t);


            return x;
        }

        public static int Delete(int ID)
        {
            string query = "DELETE TestQuestion WHERE TestQuestionID = @ID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", ID));
            return DBHelper.ModifyData(query, parameters);
        }

        public static TestQuestion SelectByPK(int ID)
        {
            string query = "SELECT * FROM TestQuestion WHERE TestQuestionID = @ID";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", ID));
            DataTable dt = DBHelper.SelectData(query, parameters);
            TestQuestion tq = new TestQuestion();
            if (dt.Rows.Count > 0)
            {
                tq.TestQuestionID = ID;
                tq.TestID = Convert.ToInt32(dt.Rows[0]["TestID"].ToString());
                tq.QuestionID = Convert.ToInt32(dt.Rows[0]["QuestionID"].ToString());
                tq.Answer = Convert.ToInt32(dt.Rows[0]["Answer"].ToString());
                tq.IsCorrect = Convert.ToInt32(dt.Rows[0]["IsCorrect"].ToString());
              
            }
            return tq;
        }

        public static DataTable SelectALL()
        {
            string query = "SELECT * FROM TestQuestion"; // WHERE StaffID=@StaffID
            List<SqlParameter> parameters = new List<SqlParameter>();
            return DBHelper.SelectData(query, parameters);
        }

        public static DataTable SelectByTestID(int TestID)
        {
            string query = "SELECT * FROM TestQuestion WHERE TestID = @TestID";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@TestID", TestID));
            return DBHelper.SelectData(query, parameters);
        }
        }
}