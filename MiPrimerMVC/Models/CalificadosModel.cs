using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiPrimerMVC.Models
{
    public class CalificadosModel 
    {
        public string producto { get; set; }
        public string categoria { get; set; }
        public double precio { get; set; }
        public string procedencia { get; set; }
        public string vendedor { get; set; }

    }
}