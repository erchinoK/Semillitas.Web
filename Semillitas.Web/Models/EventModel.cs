using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Semillitas.Web.Models
{
    public class Event
    {
        public int ID { get; set; }

        [Display(Name = "Titulo")]
        public string Title { get; set; }

        [Display(Name = "Descripcion")]
        public string Description { get; set; }

        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [AllowHtml]
        public string Content { get; set; }

        [Display(Name = "Horario")]
        public string Timetable { get; set; }
        public string TimetableNotes { get; set; }

        [Display(Name = "Ubicacion")]
        public string Location { get; set; }
        public string LocationNotes { get; set; }

        [Display(Name = "Contacto")]
        public string Contact { get; set; }
        public string ContactNotes { get; set; }
        public string FilePath { get; set; }
        public string FileNotes { get; set; }
        public string ImagePath { get; set; }
        public string ImagePreviewPath { get; set; }
        public string Layout { get; set; }

        [Display(Name = "Publicado")]
        public bool IsPublished { get; set; }

        public DateTime CreationDate { get; set; }
        public string CreationUserName { get; set; }
        public DateTime ModifDate { get; set; }
        public string ModifUserName { get; set; }
        public string Notes { get; set; }

    }
}