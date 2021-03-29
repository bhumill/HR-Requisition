using HRRequisition.DataAccess;
using HRRequisition.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace HRRequisition.Business_Logic
{
    public class SkillLogic
    { 
        public static int Insert(Skill s)
        {

            string query = "INSERT INTO Skill VALUES(@DomainID, @SkillName)";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@DomainID", s.DomainID));
            parameters.Add(new SqlParameter("@SkillName", s.SkillName));


            return DBHelper.ModifyData(query, parameters);
        }

        public static int Update(Skill s)
        {
            string query = "UPDATE Skill SET SkillName = @SkillName  WHERE SkillID = @SkillID"; //don't forget to add domain name in query
            List<SqlParameter> parameters = new List<SqlParameter>();
           // parameters.Add(new SqlParameter("@DomainID", s.DomainID));
            parameters.Add(new SqlParameter("@SkillName", s.SkillName));
            parameters.Add(new SqlParameter("@SkillID", s.SkillID));

            return DBHelper.ModifyData(query, parameters);
        }

        public static int Delete(int ID)
        {
            string query = "DELETE Skill WHERE SkillID = @ID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", ID));
            return DBHelper.ModifyData(query, parameters);
        }

        public static Skill SelectByPK(int ID)
        {
            string query = "SELECT * FROM Skill WHERE SkillID = @ID";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", ID));
            DataTable dt = DBHelper.SelectData(query, parameters);
            Skill s = new Skill();
            if (dt.Rows.Count > 0)
            {
                s.SkillID = ID;
                s.DomainID = Convert.ToInt32(dt.Rows[0]["DomainID"].ToString());
                s.SkillName = dt.Rows[0]["SkillName"].ToString();



            }
            return s;
        }

        public static DataTable SelectALL()
        {
            string query = "SELECT * FROM Skill INNER JOIN Domain ON Skill.DomainID=Domain.DomainID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            return DBHelper.SelectData(query, parameters);
        }


    }
}