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
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
    
}