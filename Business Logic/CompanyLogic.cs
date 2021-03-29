using HRRequisition.DataAccess;
using HRRequisition.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace HRRequisition.Business_Logic

{
    public class CompanyLogic
    {
        

        public static int Insert(Company c)
        {
            string query = "INSERT INTO Company VALUES(@name, @Email, @Mobile, @Username, @Password, @IsActive, @Address, @DomainID, @Status, @CompanyDocs)";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Name", c.Name));
            parameters.Add(new SqlParameter("@Email", c.Email));
            parameters.Add(new SqlParameter("@Mobile", c.Mobile));
            parameters.Add(new SqlParameter("@Username", c.Username));
            parameters.Add(new SqlParameter("@Password", c.Password));
            parameters.Add(new SqlParameter("@IsActive", c.IsActive));
            parameters.Add(new SqlParameter("@Address", c.Address));
            parameters.Add(new SqlParameter("@DomainId", c.DomainID));  
            parameters.Add(new SqlParameter("@Status", c.Status));
            parameters.Add(new SqlParameter("@CompanyDocs", c.CompanyDocs));
            


            return DBHelper.ModifyData(query, parameters);
        }

        public static int Update(Company c)
        {
            string query = "UPDATE Company SET Name = @Name, Email = @Email, Mobile = @Mobile, Username = @Username, Password = @Password, IsActive = @IsActive, Address = @Address, Status = @Status, CompanyDocs = @CompanyDocs WHERE CompanyID = @CompanyID";//DomainID
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Name", c.Name));
            parameters.Add(new SqlParameter("@Email", c.Email));
            parameters.Add(new SqlParameter("@Mobile", c.Mobile));
            parameters.Add(new SqlParameter("@Username", c.Username));
            parameters.Add(new SqlParameter("@Password", c.Password));
            parameters.Add(new SqlParameter("@IsActive", c.IsActive));
            parameters.Add(new SqlParameter("@Address", c.Address));
           // parameters.Add(new SqlParameter("@DomainID", c.DomainID));
            parameters.Add(new SqlParameter("@Status", c.Status));
            parameters.Add(new SqlParameter("@CompanyDocs", c.CompanyDocs));
            parameters.Add(new SqlParameter("@CompanyID", c.CompanyID));

            return DBHelper.ModifyData(query, parameters);
        }

        public static int Delete(int CompanyID)
        {
            string query = "DELETE Company WHERE CompanyID = @CompanyID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CompanyID", CompanyID));
            return DBHelper.ModifyData(query, parameters);
        }

        public static Company SelectByPK(int CompanyID)
        {
            string query = "SELECT * FROM Company WHERE CompanyID = @CompanyID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CompanyID", CompanyID));
            DataTable dt = GetDt(query, parameters);

            Company c = new Company();
            c.CompanyID = CompanyID;
            c.Name = dt.Rows[0]["Name"].ToString();
            c.Email = dt.Rows[0]["Email"].ToString();
            c.Mobile = dt.Rows[0]["Mobile"].ToString();
            c.Username = dt.Rows[0]["Username"].ToString();
            c.Password = dt.Rows[0]["Password"].ToString();
            c.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
            c.Address = dt.Rows[0]["Address"].ToString();
            c.DomainID = Convert.ToInt32(dt.Rows[0]["DomainID"].ToString());
            c.Status = dt.Rows[0]["Status"].ToString();
            c.CompanyDocs = dt.Rows[0]["CompanyDocs"].ToString();
           
            return c;
        }

        private static DataTable GetDt(string query, List<SqlParameter> parameters)
        {
            return DBHelper.SelectData(query, parameters);
        }


        public static DataTable SelectALL()
        {
            string query = "SELECT * FROM Company";
            List<SqlParameter> parameters = new List<SqlParameter>();
            return DataAccess.DBHelper.SelectData(query, parameters);
        }

        public static Company SelectByUnPw(string Username, string Password)
        {
            string query = "SELECT * FROM Company WHERE Username = @Username AND Password = @Password";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Username", Username));
            parameters.Add(new SqlParameter("@Password", Password));
            DataTable dt = DBHelper.SelectData(query, parameters);

            Company c = new Company();
            if (dt.Rows.Count > 0)
            {
                c.CompanyID = Convert.ToInt32(dt.Rows[0]["CompanyID"].ToString());
                c.Name = dt.Rows[0]["Name"].ToString();
                c.Email = dt.Rows[0]["Email"].ToString();
                c.Mobile = (dt.Rows[0]["Mobile"].ToString());
                c.Username = dt.Rows[0]["Username"].ToString();
                c.Password = dt.Rows[0]["Password"].ToString();
                c.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
                c.Address = dt.Rows[0]["Address"].ToString();
                c.DomainID = Convert.ToInt32(dt.Rows[0]["DomainID"].ToString());
                c.Status = dt.Rows[0]["Status"].ToString();
                c.CompanyDocs = dt.Rows[0]["CompanyDocs"].ToString();
            }
            return c;
        }

        public static DataTable SelectByStatus(String Status)
        {
            string query = @"SELECT * 
                            FROM Company
                            WHERE Status = @Status";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Status", Status));
            return DBHelper.SelectData(query, parameters);
        }
    }
}