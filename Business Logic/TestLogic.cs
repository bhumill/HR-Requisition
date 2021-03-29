using HRRequisition.DataAccess;
using HRRequisition.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HRRequisition.Business_Logic
{
    public class TestLogic
    {
        public static int Insert(Test t)
        {
            string query = @"INSERT INTO Test VALUES(@CandidateID, @StartDatetime, @CompletionDateTime, @Status, @TotalMarks, @ObtainedMarks);
                            SELECT @@IDENTITY;";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CandidateID", t.CandidateID));
            parameters.Add(new SqlParameter("@StartDatetime", t.StartDatetime));
            parameters.Add(new SqlParameter("@CompletionDateTime", t.CompletionDateTime));
            parameters.Add(new SqlParameter("@Status", t.Status));
            parameters.Add(new SqlParameter("@TotalMarks", t.TotalMarks));
            parameters.Add(new SqlParameter("@ObtainedMarks", t.ObtainedMarks));
           
            DataTable dt = DBHelper.SelectData(query, parameters);
            if(dt.Rows.Count == 1 && dt.Columns.Count == 1)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            else
            {
                return 0;
            }
        }

        public static int Update(Test t)
        {
            string query = "UPDATE Test SET StartDatetime=@StartDatetime, CompletionDateTime=@CompletionDateTime,  Status= @Status, TotalMarks=@TotalMarks, ObtainedMarks=@ObtainedMarks WHERE TestID = @TestID"; //Dont forget to add SkillID
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@StartDatetime",t.StartDatetime));
            parameters.Add(new SqlParameter("@CompletionDateTime",t.CompletionDateTime));
            parameters.Add(new SqlParameter("@Status",t.Status));
            parameters.Add(new SqlParameter("@TotalMarks",t.TotalMarks));
            parameters.Add(new SqlParameter("@ObtainedMarks",t.ObtainedMarks));
            parameters.Add(new SqlParameter("@TestID",t.TestID));


            return DBHelper.ModifyData(query, parameters);
        }

        public static int Delete(int ID)
        {
            string query = "DELETE Test WHERE TestID = @ID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", ID));
            return DBHelper.ModifyData(query, parameters);
        }

        public static Test SelectByPK(int ID)
        {
            string query = "SELECT * FROM Test WHERE TestID = @ID";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", ID));
            DataTable dt = DBHelper.SelectData(query, parameters);
            Test t = new Test();
            if (dt.Rows.Count > 0)
            {
                t.TestID = ID;
                t.CandidateID = Convert.ToInt32((dt.Rows[0]["CandidateID"].ToString()));
                t.StartDatetime = Convert.ToDateTime(dt.Rows[0]["StartDatetime"].ToString());
                t.CompletionDateTime = Convert.ToDateTime(dt.Rows[0]["CompletionDateTime"].ToString());
                t.Status = (dt.Rows[0]["Status"].ToString());
                t.TotalMarks = Convert.ToInt32(dt.Rows[0]["TotalMarks"].ToString());
                t.ObtainedMarks = Convert.ToInt32(dt.Rows[0]["ObtainedMarks"].ToString());
              



            }
            return t;
        }

        public static DataTable SelectALL()
        {
            string query = "SELECT * FROM Test";
            List<SqlParameter> parameters = new List<SqlParameter>();
            return DBHelper.SelectData(query, parameters);
        }

        public static Test SelectOngoingByCandidateID(int CandidateID)
        {
            string query = "SELECT * FROM Test WHERE CandidateID = @CandidateID AND Status = 'ONGOING'";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CandidateID", CandidateID));
            DataTable dt = DBHelper.SelectData(query, parameters);
            Test t = new Test();
            if (dt.Rows.Count > 0)
            {
                t.TestID = Convert.ToInt32((dt.Rows[0]["TestID"]));
                t.CandidateID = Convert.ToInt32((dt.Rows[0]["CandidateID"].ToString()));
                t.StartDatetime = Convert.ToDateTime(dt.Rows[0]["StartDatetime"].ToString());
                t.CompletionDateTime = Convert.ToDateTime(dt.Rows[0]["CompletionDateTime"].ToString());
                t.Status = (dt.Rows[0]["Status"].ToString());
                t.TotalMarks = Convert.ToInt32(dt.Rows[0]["TotalMarks"].ToString());
                t.ObtainedMarks = Convert.ToInt32(dt.Rows[0]["ObtainedMarks"].ToString());




            }
            return t;
        }


    }
}