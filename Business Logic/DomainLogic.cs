using HRRequisition.DataAccess;
using HRRequisition.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HRRequisition.Business_Logic
{
    public class DomainLogic
    {
        public static int Insert(Domain d)
        {

            string query = "INSERT INTO Domain VALUES(@DomainName)";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@DomainName", d.DomainName));
           

            return DBHelper.ModifyData(query, parameters);
        }

        public static int Update(Domain d)
        {
            string query = "UPDATE Domain SET DomainName = @DomainName  WHERE DomainID = @DomainID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@DomainName", d.DomainName));
            parameters.Add(new SqlParameter("@DomainID", d.DomainID));

            return DBHelper.ModifyData(query, parameters);
        }

        public static int Delete(int ID)
        {
            string query = "DELETE Domain WHERE DomainID = @ID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", ID));
            return DBHelper.ModifyData(query, parameters);
        }

        public static Domain SelectByPK(int ID)
        {
            string query = "SELECT * FROM Domain WHERE DomainID = @ID";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", ID));
            DataTable dt = DBHelper.SelectData(query, parameters);
            Domain d = new Domain();
            if (dt.Rows.Count > 0)
            {
                d.DomainID = ID;
                d.DomainName = dt.Rows[0]["DomainName"].ToString();
              

            }
            return d;
        }

        public static DataTable SelectALL()
        {
            string query = "SELECT * FROM Domain";
            List<SqlParameter> parameters = new List<SqlParameter>();
            return DBHelper.SelectData(query, parameters);
        }
    }
}