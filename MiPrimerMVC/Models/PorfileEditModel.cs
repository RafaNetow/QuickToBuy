using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiPrimerMVC.Models
{
    public class PorfileEditModel 
    {
        //
        // GET: /PorfileEditModel/
        public long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Category { get; set; }
        public virtual double Preci { get; set; }
        public virtual string UrlImage { get; set; }
        public virtual string Coin { get; set; }
        public virtual string  Account { get; set; }
	}
}