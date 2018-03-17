using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Semillitas.Web.Models.ViewModels
{
    public class HomeIndexViewModel
    {
        public virtual IList<Variable> Carousel { get; set; }

        [EmailAddress(ErrorMessage = "Por favor ingrese un email válido")]
        [Required(ErrorMessage = "Por favor ingrese un email")]
        public string Email { get; set; }

    }
    
}