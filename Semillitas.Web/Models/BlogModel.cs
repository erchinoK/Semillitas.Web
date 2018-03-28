using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Semillitas.Web.Models
{
    public class Blog
    {
        public int ID { get; set; }

        [Display(Name = "Titulo")]
        public string Title { get; set; }

        [Display(Name = "Descripcion")]
        public string Description { get; set; }

        [AllowHtml]
        public string Content { get; set; }
        public string Image { get; set; }
        public string ImagePreview { get; set; }
        public string Layout { get; set; }

        [Display(Name = "Publicado")]
        public bool IsPublished { get; set; }
        public DateTime DatePublishment { get; set; }

        public DateTime CreationDate { get; set; }
        public string CreationUserName { get; set; }
        public DateTime ModifDate { get; set; }
        public string ModifUserName { get; set; }
        public string Notes { get; set; }

    }
}