using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRRequisition.Models
{
    public class Question
    {
       public int QuestionID;
       public String Title;
       public String Option1;
       public String Option2;
       public String Option3;
       public  String Option4;
       public int SkillID;
       public int CorrectAnswer;
    }
}