using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using Domain.Services;
using Microsoft.Ajax.Utilities;
using MiPrimerMVC.Models;

namespace MiPrimerMVC.Controllers
{
    public class RegisterController : Controller
    {
        readonly IReadOnlyRepository _readOnlyRepository;
        readonly IWriteOnlyRepository _writeOnlyRepository;
        //

        public RegisterController(IReadOnlyRepository readOnlyRepository, IWriteOnlyRepository writeOnlyRepository)
        {
            _readOnlyRepository = readOnlyRepository;
            _writeOnlyRepository = writeOnlyRepository;
        }
        public RegisterController()
        {
            
        }
        public ActionResult Start()
        {
            UserModel model = (UserModel)Session["Account"];
            var user = _readOnlyRepository.FirstOrDefault<Users>(usuario => usuario.username == model.username);
            
            var usermodel = new UserModel()
            {
                Name = model.Name,
                username = model.Name,
                correo = model.correo,
                Lastname = model.Lastname,
                Id = model.Id,
                

                Productos = _readOnlyRepository.GetAll<Products>().Where(x=>x.Archived).ToList()
            
            };
            usermodel.Productos.Reverse();
            return View(usermodel);
        }
        //comentariosad
        public ActionResult profile(UserModel model)
        {
           
            var user = _readOnlyRepository.FirstOrDefault<Users>(usuario => usuario.username == model.username);
            var filters = _readOnlyRepository.GetAll<Products>().ToList();
           
            filters = filters.FindAll(x => x.username.ToUpper().Contains(model.username.ToUpper()));
            var usermodel = new UserModel()
            {
                Name =  model.Name,
                correo = model.correo,
                Lastname = model.Lastname,
               Id = model.Id,
               

                Productos = filters
                  
            };
           ;
             
            return View("~/Views/Register/profile.cshtml",usermodel);
        }

         
        public ActionResult Termes()
        {
            return View();
        }

        
        public ActionResult Addproduct()
        {
            return View(new ProductModel());
        }
        [HttpPost]
        public ActionResult Addproduct(ProductModel Model)
        {

            var Detalle = new Products();

            
              ProductModel obj = (ProductModel)TempData["myObj"];
            UserModel user = (UserModel)Session["Account"];
            Detalle.Name = Model.Name;
            Detalle.Preci = Model.Preci;
            Detalle.Category = Model.Category;
            //Detalle.UrlImage = obj.UrlImage;
            Detalle.Coin = Model.Coin;
            Detalle.username = user.username;
            
            var createdOperation = _writeOnlyRepository.Create(Detalle);
            var result = new RegisterController(_readOnlyRepository,// No recuerdo que hace esta linea de codigo
            _writeOnlyRepository).profile(user);
            return result;
        }
        public ActionResult ProductDetail(long id)
        {
            var productos = _readOnlyRepository.GetById<Products>(id);
            return View(productos);
        }
         
        public ActionResult Question()
        {
            var NewModel = new QuestionsModel();
            var Questions = _readOnlyRepository.GetAll<EQuestions>().ToList();
            NewModel.allQuestions = Questions;
            return View(NewModel);
        }
        [HttpPost]
        public ActionResult Question(QuestionsModel model)
        {
            var Quest = new EQuestions();
            UserModel user
               = new UserModel();
            
            user = (UserModel)Session["Account"];
            Quest.usuario =
            user.username;
            Quest.pregunta = model.question;
           
        
            var creaunaQuestion = _writeOnlyRepository.Create(Quest);
           
            
             return View(model);
        }
        public ActionResult PorfileEdit(long id)
        {
            var productos = _readOnlyRepository.GetById<Products>(id);
            ProductModel model = new ProductModel();
            model.Id = productos.Id;
            model.Name = productos.Name;
            model.Preci = productos.Preci;
            model.Category = productos.Category;
            model.UrlImage = productos.UrlImage;
            model.Archived = productos.Archived;
            
            return View(model);
        }

        [HttpPost]
        public ActionResult PorfileEdit(ProductModel model)
        {
            var productos = _readOnlyRepository.GetById<Products>(model.Id);

            productos.Id = model.Id;
            productos.Name = model.Name;
            productos.Preci  = model.Preci;
            productos.Category= model.Category;
            productos.UrlImage = model.UrlImage;
            if (model.Archived)
                productos.Activate();
            else
            {
                productos.Archive();
            }
            var Editado = _writeOnlyRepository.Update(productos);
            UserModel usermodel = (UserModel)Session["Account"];

            //return View("~/Views/Register/profile.cshtml",model);
            var result = new RegisterController(_readOnlyRepository,
           _writeOnlyRepository).profile(usermodel);
            return result;
            
        }
    }

}