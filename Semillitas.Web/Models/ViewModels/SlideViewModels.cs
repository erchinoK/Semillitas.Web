using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Semillitas.Web.Models.ViewModels
{
    public class SlideViewModel
    {

    }

    public class SlideCreateViewModel
    {

        public int ID { get; set; }

        // Name to use in queries. Always SLIDE
        //[Required]
        //public string Name { get; set; }

        // PUBLISHED or UNPUBLISHED
        //[Required]
        //public bool IsPublished { get; set; }

        // Path
        //[Required]
        //public string Value { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Descripcion")]
        public string Description { get; set; }

        // File
        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Imagen en español")]
        public HttpPostedFileBase ImageFileEsp { get; set; }

        //[Required(ErrorMessage = "Requerido")]
        //[Display(Name = "Imagen en catalan")]
        //public HttpPostedFileBase ImageFileCat { get; set; }

        //[Required(ErrorMessage = "Requerido")]
        //[Display(Name = "Imagen en ingles")]
        //public HttpPostedFileBase ImageFileEng { get; set; }

        [AllowHtml]
        public string Html { get; set; }

    }

    public class SlideEditViewModel
    {

        public int ID { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Descripcion")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Posicion")]
        public int Position { get; set; }

        [Display(Name = "Publicado")]
        public bool IsPublished { get; set; }

        [AllowHtml]
        public string Html { get; set; }

    }
}