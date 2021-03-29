using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRRequisition.Models
{
    public class Comment
    {
      public  int CommentID;
      public  String CommentText;
      public  int CandidateID;
      public  DateTime CommentDate;
      public int TopicID;
    }
}