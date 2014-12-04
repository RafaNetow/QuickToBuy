using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;

namespace MiPrimerMVC.Models
{
    public class QuestionsModel 
    {
        //
        // GET: /QuestionsModel/
        public virtual long Id { get; set; }
        public virtual string usario { get; set; }
        public virtual string question { get; set; }
        public virtual string answer { get; set; }
        public List<EQuestions> allQuestions { get; set; } 
     
	}
}