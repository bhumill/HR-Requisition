using HRRequisition.DataAccess;
using HRRequisition.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
 namespace HRRequisition.Business_Logic
{
    public class StaffLogic
    { 
        public static int Insert(Staff s)
        {   
          
            string query = "INSERT INTO Staff VALUES(@Name, @Email, @Mobile, @Username, @Password, @IsActive, @StaffType, @DomainID)";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Name", s.Name));
            parameters.Add(new SqlParameter("@Email", s.Email));
            parameters.Add(new SqlParameter("@Mobile", s.Mobile));
            parameters.Add(new SqlParameter("@Username", s.Username));
            parameters.Add(new SqlParameter("@Password", s.Password));
            parameters.Add(new SqlParameter("@IsActive", s.IsActive));
            parameters.Add(new SqlParameter("@StaffType", s.StaffType));
            parameters.Add(new SqlParameter("@DomainID", s.DomainID));

            return DBHelper.ModifyData(query, parameters);
        }

        public static int Update(Staff s)
        {
            string query = "UPDATE Staff SET Name = @Name, Email = @Email, Mobile = @Mobile,  Username = @Username, Password = @Password, IsActive = @IsActive, StaffType = @StaffType  WHERE StaffID = @StaffID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Name", s.Name));
            parameters.Add(new SqlParameter("@Email", s.Email));
            parameters.Add(new SqlParameter("@Mobile", s.Mobile));
            parameters.Add(new SqlParameter("@Username", s.Username));
            parameters.Add(new SqlParameter("@Password", s.Password));
            parameters.Add(new SqlParameter("@IsActive", s.IsActive));
            parameters.Add(new SqlParameter("@StaffType", s.StaffType));
            parameters.Add(new SqlParameter("@StaffID", s.StaffID));


            return DBHelper.ModifyData(query, parameters);
        }

        public static int Delete(int ID)
        {
            string query = "DELETE Staff WHERE StaffID = @ID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", ID));
            return DBHelper.ModifyData(query, parameters);
        }

        public static Staff SelectByPK(int ID)
        {
            string query = "SELECT * FROM Staff WHERE StaffID = @ID";
            
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", ID));
            DataTable dt = DBHelper.SelectData(query, parameters);
            Staff s = new Staff();
            if (dt.Rows.Count > 0)
            {
                s.StaffID = ID;
                s.Name = dt.Rows[0]["Name"].ToString();
                s.Email = dt.Rows[0]["Email"].ToString();
                s.Mobile = dt.Rows[0]["Mobile"].ToString();
                s.Username = dt.Rows[0]["Username"].ToString();
                s.Password = dt.Rows[0]["Password"].ToString();
                s.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
                s.StaffType = dt.Rows[0]["StaffType"].ToString();
                s.DomainID = Convert.ToInt32(dt.Rows[0]["DomainID"].ToString());

            }
            return s;
        }

        internal static DataTable SelectByPK(string v)
        {
            throw new NotImplementedException();
        }

        public static DataTable SelectALL()
        {
            string query = "SELECT * FROM Staff"; // WHERE StaffID=@StaffID
            List<SqlParameter> parameters = new List<SqlParameter>();
            return DBHelper.SelectData(query, parameters);
        }

        public static Staff SelectByUnPw(string Username, string Password)
        {
            string query = "SELECT * FROM Staff WHERE Username = @Username AND Password = @Password";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Username", Username));
            parameters.Add(new SqlParameter("@Password", Password));
            DataTable dt = DBHelper.SelectData(query, parameters);

            Staff s = new Staff();
            if (dt.Rows.Count > 0)
            {
                s.StaffID = Convert.ToInt32(dt.Rows[0]["StaffID"].ToString());
                s.Name = dt.Rows[0]["Name"].ToString();
                s.Email = dt.Rows[0]["Email"].ToString();
                s.Mobile = dt.Rows[0]["Mobile"].ToString();
                s.Username = dt.Rows[0]["Username"].ToString();
                s.Password = dt.Rows[0]["Password"].ToString();
                s.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
                s.StaffType = dt.Rows[0]["StaffType"].ToString();
               
            }
            return s;
        }
    }
}