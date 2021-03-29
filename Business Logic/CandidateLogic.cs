using HRRequisition.DataAccess;
using HRRequisition.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace HRRequisition.Business_Logic
{
    public class CandidateLogic
    {
        public static SqlDbType ID { get; private set; }

        public static int Insert(Candidate c)
        {
            string query = "INSERT INTO Candidate VALUES(@Name, @Email, @Mobile, @Username, @Password, @IsActive,  @Address, @City, @DomainID,  @ResumeFile, @Remarks, @ProfilePicture, @RegisterDate, @Experience)";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Name", c.Name));
            parameters.Add(new SqlParameter("@Email", c.Email));
            parameters.Add(new SqlParameter("@Mobile", c.Mobile));
            parameters.Add(new SqlParameter("@Username", c.Username));
            parameters.Add(new SqlParameter("@Password", c.Password));
            parameters.Add(new SqlParameter("@IsActive", c.IsActive));
            parameters.Add(new SqlParameter("@Address", c.Address));
            parameters.Add(new SqlParameter("@City", c.City));
            parameters.Add(new SqlParameter("@DomainId", c.DomainID));
            parameters.Add(new SqlParameter("@ResumeFile", c.ResumeFile));
            parameters.Add(new SqlParameter("@Remarks", c.Remarks));
            parameters.Add(new SqlParameter("@ProfilePicture", c.ProfilePicture));
            parameters.Add(new SqlParameter("@RegisterDate", c.RegisterDate));
            parameters.Add(new SqlParameter("@Experience", c.Experience));


            return DBHelper.ModifyData(query, parameters);
        }

        public static int Update(Candidate c)
        {
            string query = "UPDATE Candidate SET Name = @Name, Email = @Email, Mobile = @Mobile,  Username = @Username, Password = @Password, IsActive = @IsActive, Address = @Address, City = @City, DomainID=@DomainID, ResumeFile = @ResumeFile, Remarks = @Remarks,  ProfilePicture= @ProfilePicture, RegisterDate = @RegisterDate   WHERE CandidateID = @CandidateID";  // Don't fprget to add domain ID
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Name", c.Name));
            parameters.Add(new SqlParameter("@Email", c.Email));
            parameters.Add(new SqlParameter("@Mobile", c.Mobile));
            parameters.Add(new SqlParameter("@Username", c.Username));
            parameters.Add(new SqlParameter("@Password", c.Password));
            parameters.Add(new SqlParameter("@IsActive", c.IsActive));
            parameters.Add(new SqlParameter("@Address", c.Address));
            parameters.Add(new SqlParameter("@City", c.City));
            parameters.Add(new SqlParameter("@DomainId", c.DomainID));
            parameters.Add(new SqlParameter("@ResumeFile", c.ResumeFile));
            parameters.Add(new SqlParameter("@Remarks", c.Remarks));
            parameters.Add(new SqlParameter("@ProfilePicture", c.ProfilePicture));
            parameters.Add(new SqlParameter("@RegisterDate", c.RegisterDate));
            parameters.Add(new SqlParameter("@Experience", c.Experience));
            parameters.Add(new SqlParameter("@CandidateID", c.CandidateID));

            return DBHelper.ModifyData(query, parameters);
        }

        public static int Delete(int CandidateID)
        {
            string query = "DELETE Candidate WHERE CandidateID = @CandidateID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CandidateID", CandidateID));
            return DBHelper.ModifyData(query, parameters);
        }

        public static Candidate SelectByPK(int CandidateID)
        {
            string query = "SELECT * FROM Candidate WHERE CandidateID = @CandidateID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CandidateID", CandidateID));
            DataTable dt = GetDt(query, parameters);

            Candidate c = new Candidate();
            c.CandidateID = CandidateID;
            c.Name = dt.Rows[0]["Name"].ToString();
            c.Email = dt.Rows[0]["Email"].ToString();
            c.Mobile = (dt.Rows[0]["Mobile"].ToString());
            c.Address = dt.Rows[0]["Address"].ToString();
            c.City = dt.Rows[0]["City"].ToString();
            c.Username = dt.Rows[0]["Username"].ToString();
            c.Password = dt.Rows[0]["Password"].ToString();
            c.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
            c.DomainID = Convert.ToInt32(dt.Rows[0]["DomainID"].ToString());
            c.ResumeFile = dt.Rows[0]["ResumeFile"].ToString();
            c.Remarks = dt.Rows[0]["Remarks"].ToString();
            c.ProfilePicture = dt.Rows[0]["ProfilePicture"].ToString();
            c.RegisterDate = Convert.ToDateTime(dt.Rows[0]["RegisterDate"].ToString());
            c.Experience = Convert.ToInt32(dt.Rows[0]["Experience"].ToString());
            return c;
        }

        private static DataTable GetDt(string query, List<SqlParameter> parameters)
        {
            return DBHelper.SelectData(query, parameters);
        }

        public static DataTable SelectALL()
        {
            string query = "SELECT * FROM Candidate";
            List<SqlParameter> parameters = new List<SqlParameter>();
            return DataAccess.DBHelper.SelectData(query, parameters);
        }

        public static Candidate SelectByUnPw(string Username, string Password)
        {
            string query = "SELECT * FROM Candidate WHERE Username = @Username AND Password = @Password";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Username", Username));
            parameters.Add(new SqlParameter("@Password", Password));
            DataTable dt = DBHelper.SelectData(query, parameters);

            Candidate c = new Candidate();
            if (dt.Rows.Count > 0)
            {
                c.CandidateID = Convert.ToInt32(dt.Rows[0]["CandidateID"].ToString());
                c.Name = dt.Rows[0]["Name"].ToString();
                c.Email = dt.Rows[0]["Email"].ToString();
                c.Mobile = (dt.Rows[0]["Mobile"].ToString());
                c.Address = dt.Rows[0]["Address"].ToString();
                c.City = dt.Rows[0]["City"].ToString();
                c.Username = dt.Rows[0]["Username"].ToString();
                c.Password = dt.Rows[0]["Password"].ToString();
                c.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
                // c.DomainID = Convert.ToInt32(dt.Rows[0]["DomainID"].ToString());
                c.ResumeFile = dt.Rows[0]["ResumeFile"].ToString();
                c.Remarks = dt.Rows[0]["Remarks"].ToString();
                c.ProfilePicture = dt.Rows[0]["ProfilePicture"].ToString();
                c.RegisterDate = Convert.ToDateTime(dt.Rows[0]["RegisterDate"].ToString());
                c.Experience = Convert.ToInt32(dt.Rows[0]["Experience"].ToString());    

            }
            return c;
        }

        public static DataTable SearchMatching(int VacancyID)
        {
            Vacancy v = VacancyLogic.SelectByPK(VacancyID);
            DataTable dtVskills = VacancySkillLogic.SelectByVacancyID(VacancyID);

            string query = @"SELECT C.* 
                            FROM Candidate C
                            WHERE (C.Experience >= @MinExperience)
                                AND @MinMarks <= (SELECT TOP 1 (100.0 * T.ObtainedMarks / T.TotalMarks) FROM Test T WHERE T.CandidateID = C.CandidateID ORDER BY TestID DESC)";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@VacancyID", v.VacancyID));
            parameters.Add(new SqlParameter("@MinMarks", v.MinMarks));
            parameters.Add(new SqlParameter("@MinExperience", v.MinExperience));
            DataTable dtCan = GetDt(query, parameters);

            dtCan.Columns.Add("Matching", typeof(string));

            for (int i = 0; i < dtCan.Rows.Count; i++)
            {
                DataTable dtCskills = CandidateSkillLogic.SelectByCandidateID(Convert.ToInt32(dtCan.Rows[i]["CandidateID"]));
                int matching = 0;
                for (int j = 0; j < dtVskills.Rows.Count; j++)
                {
                    if(dtCskills.Select("SkillID = " + dtVskills.Rows[j]["SkillID"].ToString()).Length > 0)
                    {
                        matching++;
                    }
                }
                if(matching <dtVskills.Rows.Count)
                {
                    dtCan.Rows[i]["Matching"] = "Below";
                }
                else if (matching > dtVskills.Rows.Count)
                {
                    dtCan.Rows[i]["Matching"] = "Above";
                }
                else if (matching == dtVskills.Rows.Count)
                {
                    dtCan.Rows[i]["Matching"] = "Exact";
                }
            }

            return dtCan;
        }

    }
}


