using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;

namespace MiPrimerMVC.Models
{
    public class UserModel 
    {
        //
        // GET: /UserModel/
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name requerid")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Lastname requerid")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Username requerid")]
        public string username { get; set; }
        [Required(ErrorMessage = "email requerid")]
        public string correo { get; set; }
        [Required(ErrorMessage = "password requerid")]
        public string password { get; set; }
        [Required(ErrorMessage = "ConfirmationPassword requerid")]
        public string Confirmationpassword { get; set; }

        public List<Products> Productos { get; set; } 
        
    }
}