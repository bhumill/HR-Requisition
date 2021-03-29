using HRRequisition.DataAccess;
using HRRequisition.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HRRequisition.Business_Logic
{
    public class VacancySkillLogic
    {
        public static int Insert(VacancySkill v)
        {
            string query = "INSERT INTO VacancySkill VALUES(@VacancyID, @SkillID)";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@VacancyID", v.VacancyID));
            parameters.Add(new SqlParameter("@SkillID", v.SkillID));
           



            return DBHelper.ModifyData(query, parameters);
        }

        public static int Update(VacancySkill v)
        {
            string query = "UPDATE VacancySkill SET VacancyID=@VacancyID, SkillID = @SkillID WHERE VacancySkillID = @VacancySkillID";  // Don't fprget to add domain ID,staffID CompanyID=@CompanyID,
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@VacancyID", v.VacancyID));
            parameters.Add(new SqlParameter("@SkillID", v.SkillID));
            parameters.Add(new SqlParameter("@VacancySkillID", v.VacancySkillID));


            return DBHelper.ModifyData(query, parameters);

        }

        public static int Delete(int ID)
        {
            string query = "DELETE VacancySkill WHERE VacancySkillID = @ID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", ID));
            return DBHelper.ModifyData(query, parameters);
        }

        public static VacancySkill SelectByPK(int VacancySkillID)
        {
            string query = "SELECT * FROM VacancySkill WHERE VacancySkillID = @VacancySkillID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@VacancySkillID", VacancySkillID));
            DataTable dt = GetDt(query, parameters);

            VacancySkill v = new VacancySkill();
            v.VacancySkillID = VacancySkillID;
            v.VacancyID = Convert.ToInt32(dt.Rows[0]["VacancyID"].ToString());
            v.SkillID = Convert.ToInt32(dt.Rows[0]["SkillID"].ToString());
            return v;
        }

        private static DataTable GetDt(string query, List<SqlParameter> parameters)
        {
            return DBHelper.SelectData(query, parameters);
        }

        public static DataTable SelectALL()
        {
            string query = "SELECT * FROM VacancySkill";
            List<SqlParameter> parameters = new List<SqlParameter>();
            return DataAccess.DBHelper.SelectData(query, parameters);
        }

        public static DataTable SelectByVacancyID(int VacancyID)
        {
            String query = @"SELECT * FROM VacancySkill INNER JOIN Skill ON Skill.SkillID=VacancySkill.SkillID WHERE VacancyID=@VacancyID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@VacancyID", VacancyID));
            return DBHelper.SelectData(query, parameters);
        }
    }
}