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
        public string Title { get; set; }
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [AllowHtml]
        public string Content { get; set; }
        public string Timetable { get; set; }
        public string TimetableNotes { get; set; }
        public string Location { get; set; }
        public string LocationNotes { get; set; }
        public string Contact { get; set; }
        public string ContactNotes { get; set; }
        public string Pdf { get; set; }
        public string PdfNotes { get; set; }
        public string Image { get; set; }
        public string ImagePreview { get; set; }
        public string Layout { get; set; }
        public bool IsPublished { get; set; }

        public DateTime CreationDate { get; set; }
        public string CreationUserName { get; set; }
        public DateTime ModifDate { get; set; }
        public string ModifUserName { get; set; }
        public string Notes { get; set; }

    }
}