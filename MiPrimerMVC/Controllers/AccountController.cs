using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using Domain.Services;
using MiPrimerMVC.Models;

namespace MiPrimerMVC.Controllers
{
    public class AccountController : Controller
    {
        readonly IReadOnlyRepository _readOnlyRepository;
        readonly IWriteOnlyRepository _writeOnlyRepository;
        //

        public AccountController(IReadOnlyRepository readOnlyRepository, IWriteOnlyRepository writeOnlyRepository)
        {
            _readOnlyRepository = readOnlyRepository;
            _writeOnlyRepository = writeOnlyRepository;
        }
        public ActionResult Login()
        {
            return View(new AccountLoginModel());
        }
	     [HttpPost]
        public ActionResult Login(AccountLoginModel model)
	     {
	         string Admin = "Admin";
	         string pas = "12";
             if (model.Email.Equals("Admin") && model.Password.Equals("12"))
	         {
	            UserModel modeluser = new UserModel();
	             modeluser.Name = model.Email;
                 var results = new RegisterController(_readOnlyRepository, // No recuerdo que hace esta linea de codigo
	                 _writeOnlyRepository).AdminView(modeluser);
	             return results;
	         } 
	         
                 var user= _readOnlyRepository.FirstOrDefault<Users>(usuario => usuario.correo == model.Email);
             user= _readOnlyRepository.FirstOrDefault<Users>(usuario => usuario.username == model.Email);
             var Questions = _readOnlyRepository.GetAll<EQuestions>().ToList();
             var usermodel = new UserModel();
             var modelQuestion = new QuestionsModel();	        
              

             if (user == null || user.password != model.Password  )
                 return View("~/Views/Home/Index.cshtml");
             

	         usermodel.Name = user.Name;           
             usermodel.Lastname = user.Lastname;
	         usermodel.correo = user.correo;
	         usermodel.username = user.username;
             usermodel.password = user.password;
	         modelQuestion.allQuestions = Questions;
             Session["Account"] = usermodel;

	         
             
             Session["EQuestion"] = Questions;
             UserModel prueba = (UserModel)Session["Account"];
	         
             //return View("~/Views/Register/profile.cshtml",model);
             var result = new RegisterController(_readOnlyRepository,
            _writeOnlyRepository).profile(usermodel);
	         return result;
	     }
    }
}