using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRRequisition.Models
{
    public class Candidate
    {
        public  int CandidateID;
        public String Name;
        public String Email;
        public String Mobile;
        public String Username;
        public String Password;
        public Boolean IsActive;
        public int DomainID;
        public String Address;
        public String City;
        public String ResumeFile;
        public String Remarks;
        public String ProfilePicture;
        public DateTime RegisterDate;
        public int Experience;

    }
}