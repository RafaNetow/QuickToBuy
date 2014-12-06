using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using Domain.Services;
using MiPrimerMVC.Models;

namespace MiPrimerMVC.Controllers
{
    public class HomeController : Controller
    {
       
        readonly IReadOnlyRepository _readOnlyRepository;
        readonly IWriteOnlyRepository _writeOnlyRepository;
        //

        public HomeController(IReadOnlyRepository readOnlyRepository, IWriteOnlyRepository writeOnlyRepository)
        {
            _readOnlyRepository = readOnlyRepository;
            _writeOnlyRepository = writeOnlyRepository;
        }
            
        public ActionResult Index()
        {
           
            return View();
        }




        public ActionResult SignUp()
        {
            return View(new UserModel());
        }

         [HttpPost]
        public ActionResult SignUp(UserModel Model)
         {

             var user = new Users();

             
             user.Name = Model.Name;
             user.Lastname = Model.Lastname;
             user.correo = Model.correo;
             user.password = Model.password;
             user.username = Model.username;
             var createdOperation = _writeOnlyRepository.Create(user);
             return View();
        }
    }
}