using HRRequisition.DataAccess;
using HRRequisition.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HRRequisition.Business_Logic
{
    public class CandidateSkillLogic
    {
        public static int Insert(CandidateSkill cs)
        {   

            string query = "INSERT INTO CandidateSkill VALUES(@CandidateID, @SkillID, @Remarks)";
            List<SqlParameter> parameters = new List<SqlParameter>();
           parameters.Add(new SqlParameter("@CandidateID", cs.CandidateID));
            parameters.Add(new SqlParameter("@SkillID", cs.SkillID));
           // parameters.Add(new SqlParameter("@SkillName", cs.SkillName));
            parameters.Add(new SqlParameter("@Remarks", cs.Remarks));


            return DBHelper.ModifyData(query, parameters);
        }

        public static int Update(CandidateSkill cs)
        {
            string query = "UPDATE CandidateSkill SET CandidateID=@CandidateID, Remarks=@Remarks  WHERE CandidateSkillID = @CandidateSkillID"; //domainID,SkillID
            List<SqlParameter> parameters = new List<SqlParameter>();
             parameters.Add(new SqlParameter("@CandidateID", cs.CandidateID));
            // parameters.Add(new SqlParameter("@SkillID", cs.SkillID));
            //parameters.Add(new SqlParameter("@SkillName", cs.SkillName));
            parameters.Add(new SqlParameter("@Remarks", cs.Remarks));
            parameters.Add(new SqlParameter("@CandidateSkillID", cs.CandidateSkillID));
            return DBHelper.ModifyData(query, parameters);
        }

        public static int Delete(int ID)
        {
            string query = "DELETE CandidateSkill WHERE CandidateSkillID = @ID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", ID));
            return DBHelper.ModifyData(query, parameters);
        }

        public static CandidateSkill SelectByPK(int ID)
        {
            string query = "SELECT * FROM CandidateSkill WHERE CandidateSkillID = @ID";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", ID));
            DataTable dt = DBHelper.SelectData(query, parameters);
            CandidateSkill cs = new CandidateSkill();
            if (dt.Rows.Count > 0)
            {
                cs.CandidateSkillID = ID;
                cs.CandidateID = Convert.ToInt32(dt.Rows[0]["CandidateID"].ToString());
                cs.SkillID = Convert.ToInt32(dt.Rows[0]["SkillID"].ToString());
                //cs.Remarks = dt.Rows[0]["SkillName"].ToString();
                cs.Remarks = dt.Rows[0]["Remarks"].ToString();



            }
            return cs;
        }
        public static DataTable SelectALL()
        {
            string query = "SELECT * FROM CandidateSkill";
            List<SqlParameter> parameters = new List<SqlParameter>();
            return DBHelper.SelectData(query, parameters);
        }

        public static DataTable SelectByCandidateID(int CandidateID)
        {
            //string query = @"SELECT * 
            //                FROM CandidateSkill
            //                WHERE CandidateID = @CandidateID";

            String query = @"SELECT * FROM CandidateSkill INNER JOIN Skill ON Skill.SkillID=CandidateSkill.SkillID WHERE CandidateID=@CandidateID";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CandidateID", CandidateID));
            return DBHelper.SelectData(query, parameters);
        }

       public static DataTable SelectBySkillID(int SkillID)
        {
            String query = @"SELECT * FROM CandidateSkill INNER JOIN Skill ON Skill.SkillID=CandidateSkill.SkillID WHERE CandidateID=@CandidateID";
           List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SkillID", SkillID));
            return DBHelper.SelectData(query, parameters);
        }
    }
}