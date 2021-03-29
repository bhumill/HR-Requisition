using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRRequisition.Models
{
    public class Topic
    {
       public int TopicID;
       public String Title;
      public  String Description;
      public  int CandidateID;
       public int DomainID;
       public DateTime StartDate;
    }
}