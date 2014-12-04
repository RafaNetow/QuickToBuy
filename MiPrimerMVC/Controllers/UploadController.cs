using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using Domain.Services;
using MiPrimerMVC.Controllers;
using MiPrimerMVC.Models;
using MiPrimerMVC.ProyectHelpers;
using System.Net.Configuration;
using Microsoft.Ajax.Utilities;
using Ninject;

namespace MiPrimerMVC.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
          
        //
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(ProductModel model)
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                
                if (file != null && file.ContentLength > 0)
                {

                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Image/"), fileName);

                    //  var path = Path.GetDirectoryName(file.FileName);

                    UserModel user = (UserModel)Session["Account"];
                    var urlImage = (string)ImgurUpload.UploadImage(path);
                    char[] a = urlImage.ToCharArray();
                    string url = "";
                    var newurl =ArreglarUrl(a, url);
                    model.UrlImage = url;


                    model.UrlImage= newurl;
                    model.username = user.username;
                    TempData["myObj"] = model;
                    
                 

                    
                }
              
            }

            return RedirectToAction("Addproduct", "Register");
        }

        public ActionResult UploadDocument()
        {
            throw new NotImplementedException();
        }

        public static string ArreglarUrl(char[] urlmala,string urlbuena )
        {
            for (int i = 5; i < urlmala.Length; i++)
            {
                urlbuena = urlbuena + urlmala[i].ToString();
            }
            return urlbuena;
        }
    }

    
}