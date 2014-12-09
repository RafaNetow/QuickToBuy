using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;

namespace MiPrimerMVC.Models
{
    public class AdminModel : Controller
    {
        //
        // GET: /AdminModel/
        public List<Reportado> ListaDeProductosReportados { get; set; }
        public List<ProductModel> ListaDeModelos { get; set; } 
        public List<MensajesTwilio> MensajesTwilio { get;set;}
        }
	}
