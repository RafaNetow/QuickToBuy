using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using Domain.Entities;
using Domain.Services;
using MiPrimerMVC.Models;
using MiPrimerMVC.ProyectHelpers;
using NHibernate.Driver;
using NHibernate.Hql.Ast.ANTLR;

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

            var usermodel = new UserModel(); 
            var user = _readOnlyRepository.FirstOrDefault<Users>(usuario => usuario.username == model.username);
           
            var filters = _readOnlyRepository.GetAll<Products>().ToList();
           if(model.username != null)
           {
               var Seguidores = _readOnlyRepository.GetAll<Follows>().Where(x => x.Seguido.username == model.username).ToList();
               

             

               filters = filters.FindAll(x => x.username.ToUpper().Contains(model.username.ToUpper()));
          
            usermodel = new UserModel()
            {
                Name =  model.Name,
                correo = model.correo,
                Lastname = model.Lastname,
               Id = model.Id,
               

                Productos = filters
                  
            };
          } 
               else
            {
              var usuarioactual = (UserModel)Session["Account"];
              filters = filters.FindAll(x => x.username.ToUpper().Contains(usuarioactual.username.ToUpper()));
               usermodel = new UserModel()
              {
                  Name = usuarioactual.Name,
                  correo = usuarioactual.correo,
                  Lastname = usuarioactual.Lastname,
                  Id = usuarioactual.Id,


                  Productos = filters

              };
            }
   
           
             
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
           if(obj != null)
            Detalle.UrlImage = obj.UrlImage;
            Detalle.Coin = Model.Coin;
            Detalle.username = user.username;
            var Seguidores = _readOnlyRepository.GetAll<Follows>().ToList();



            
           
            if (Seguidores.Count>0) { 
                var mensaje = new ApiTwilio();
                mensaje.mensajeTwilio("El comerciante"+Detalle.username +"Ah Creado un producto llamado"+Detalle.Name);
            }
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

        public ActionResult Reportados()
        {
         ProductReportModel model = new ProductReportModel();
            var productosReportados  =  _readOnlyRepository.GetAll<Reportado>().ToList();
            model.ListaDeProductosReportados = productosReportados;
            return View(model);
        }
        public ActionResult CrearReportado(long id)
        {
            UserModel usermodel = (UserModel)Session["Account"];
            var productos = _readOnlyRepository.GetById<Products>(id);
            Users user = new Users();
            user.Name = usermodel.Name;
            user.password = usermodel.Name;
            user.correo = usermodel.correo;
          

            Reportado productrepor = new Reportado();
            productrepor.Producto = productos;
            productrepor.Usuario = user;
            var ProductoReportado = _writeOnlyRepository.Create(productrepor);
            return RedirectToAction("Start");
        }
        public ActionResult UsuarioVendedor(long id)
        {
            var productos = _readOnlyRepository.GetById<Products>(id);
            var model = _readOnlyRepository.FirstOrDefault<Users>(usuario => usuario.username == productos.username);
            UserModel usuariologeado = (UserModel)Session["Account"];
            var Prod = _readOnlyRepository.GetAll<Products>().Where(x => x.Archived).ToList();
            Prod = Prod.FindAll(x => x.username.ToUpper().Contains(productos.username.ToUpper()));
            var usermodel = new UserModel()
            {
                Name = model.Name,
                username = model.Name,
                correo = model.correo,
                Lastname = model.Lastname,
               Productos =  Prod


                
            }; 
            return View(usermodel);
        }


        
        public ActionResult followers(long id )
        {
            Follows ObjetoFollowers = new Follows();
               //Objeto Usuario Seguidor
                   UserModel userlog = (UserModel)Session["Account"];
                   Users usuariologeado = new Users();       
                    usuariologeado.Name = userlog.Name;
                   usuariologeado.Lastname = userlog.Lastname;
                   usuariologeado.correo = userlog.correo;
                   usuariologeado.username = userlog.username;
                   usuariologeado.password = userlog.password;
            //Objeto Usuario Seguido
                   var productos = _readOnlyRepository.GetById<Products>(id);
                 
                     
             Users userEntidad = new Users();
                  userEntidad.username = productos.username;
                   //userEntidad.Lastname = model.Lastname;
                   //userEntidad.correo = model.correo;
                   //userEntidad.username = model.username;
                   //userEntidad.password = model.password;

            ObjetoFollowers.Seguido = userEntidad;
            ObjetoFollowers.Seguidor = usuariologeado;

            var AccionDeSeguir = _writeOnlyRepository.Create(ObjetoFollowers);
            
            return RedirectToAction("profile");
        }
        public static string ArreglarUrl(char[] urlmala, string urlbuena)
        {
            for (int i = 5; i < urlmala.Length; i++)
            {
                urlbuena = urlbuena + urlmala[i].ToString();
            }
            return urlbuena;
        }
    public ActionResult SendTwilio ()
    {

        return View(new TwilioModel());
    }
    [HttpPost] 
    public ActionResult SendTwilio ( TwilioModel model)
    {
        ApiTwilio twilio = new ApiTwilio();
          twilio.mensajeTwilio(model.Name);
        MensajesTwilio mensaje = new MensajesTwilio();
        mensaje.mensaje = model.Name;
        var SaveMessage = _writeOnlyRepository.Create(mensaje);
        return RedirectToAction("Start");
    }

        public ActionResult AdminView (UserModel model)
        {
            return View("~/Views/Register/AdminView.cshtml");
        }
    }

}