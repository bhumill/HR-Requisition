using HRRequisition.DataAccess;
using HRRequisition.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HRRequisition.Business_Logic
{
    public class VacancyLogic
    {
        public static SqlDbType ID { get; private set; }

        public static int Insert(Vacancy v)
        {
            string query = "INSERT INTO Vacancy VALUES(@CompanyID, @MinMarks, @Requirements, @ContractDocs, @MinExperience, @Status, @StaffID)";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CompanyID", v.CompanyID));
            parameters.Add(new SqlParameter("@MinMarks", v.MinMarks));
            parameters.Add(new SqlParameter("@Requirements", v.Requirements));
            parameters.Add(new SqlParameter("@ContractDocs", v.ContractDocs));
            parameters.Add(new SqlParameter("@MinExperience", v.MinExperience));
            parameters.Add(new SqlParameter("@Status", v.Status));
            parameters.Add(new SqlParameter("@StaffID", v.StaffID));
            


            return DBHelper.ModifyData(query, parameters);
        }

        internal static DataTable SelectByStatus(object companyID)
        {
            throw new NotImplementedException();
        }

        public static int Update(Vacancy v)
        {
            string query = "UPDATE Vacancy SET CompanyID=@CompanyID, MinMarks = @MinMarks, Requirements = @Requirements, ContractDocs = @ContractDocs,  MinExperience = @MinExperience, Status = @Status WHERE VacancyID = @VacancyID";  // Don't fprget to add domain ID,staffID CompanyID=@CompanyID,
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CompanyID", v.CompanyID));
            parameters.Add(new SqlParameter("@MinMarks", v.MinMarks));
            parameters.Add(new SqlParameter("@Requirements", v.Requirements));
            parameters.Add(new SqlParameter("@ContractDocs", v.ContractDocs));
            parameters.Add(new SqlParameter("@MinExperience", v.MinExperience));
            parameters.Add(new SqlParameter("@Status", v.Status));
            // parameters.Add(new SqlParameter("@StaffID", v.StaffID));
            parameters.Add(new SqlParameter("@VacancyID", v.VacancyID));


            return DBHelper.ModifyData(query, parameters);
        
        }

        public static int Delete(int ID)
        {
            string query = "DELETE Vacancy WHERE VacancyID = @ID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", ID));
            return DBHelper.ModifyData(query, parameters);
        }

        public static Vacancy SelectByPK(int VacancyID)
        {
            string query = "SELECT * FROM Vacancy WHERE VacancyID = @VacancyID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@VacancyID", VacancyID));
            DataTable dt = GetDt(query, parameters);

            Vacancy v = new Vacancy();
            v.VacancyID = VacancyID;
            v.CompanyID = Convert.ToInt32(dt.Rows[0]["CompanyID"].ToString());
            v.MinMarks = Convert.ToInt32(dt.Rows[0]["MinMarks"].ToString());
            v.Requirements = (dt.Rows[0]["Requirements"].ToString());
            v.ContractDocs = dt.Rows[0]["ContractDocs"].ToString();
            v.MinExperience = Convert.ToInt32(dt.Rows[0]["MinExperience"]);
            v.Status = dt.Rows[0]["Status"].ToString();
            v.StaffID = Convert.ToInt32(dt.Rows[0]["StaffID"].ToString());
            return v;
        }

        private static DataTable GetDt(string query, List<SqlParameter> parameters)
        {
            return DBHelper.SelectData(query, parameters);
        }



        public static DataTable SelectALL()
        {
            string query = "SELECT * FROM Vacancy";
            List<SqlParameter> parameters = new List<SqlParameter>();
            return DataAccess.DBHelper.SelectData(query, parameters);
        }

        public static DataTable SelectByStatus(String Status)
        {
            string query = @"SELECT * 
                            FROM Vacancy
                            WHERE Status = @Status";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Status", Status));
            return DBHelper.SelectData(query, parameters);
        }

        public static DataTable SelectByCompanyID(int CompanyID)
        {
            string query = @"SELECT * 
                            FROM Vacancy
                            WHERE CompanyID = @CompanyID";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CompanyID", CompanyID));
            return DBHelper.SelectData(query, parameters);
        }
    }
}