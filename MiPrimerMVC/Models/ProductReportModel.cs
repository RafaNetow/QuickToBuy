using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;

namespace MiPrimerMVC.Models
{
    public class ProductReportModel
    {
        //
        // GET: /ProductReportModel/
        public List<Reportado> ListaDeProductosReportados { get; set; } 
	}
}