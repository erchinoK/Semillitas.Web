using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Semillitas.Web.Models.ViewModels
{
    public class EventCreateViewModel
    {
        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Titulo")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Descripcion")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }

        [AllowHtml]
        [Display(Name = "Contenido")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Horario")]
        public string Timetable { get; set; }

        [Display(Name = "Descripcion de horario")]
        public string TimetableNotes { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Ubicacion")]
        public string Location { get; set; }

        [Display(Name = "Descripcion de ubicacion")]
        public string LocationNotes { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Contacto")]
        public string Contact { get; set; }

        [Display(Name = "Descripcion de contacto")]
        public string ContactNotes { get; set; }

        [Display(Name = "Archivo descargarble")]
        public HttpPostedFileBase File { get; set; }

        [Display(Name = "Descripcion de archivo descargable")]
        public string FileNotes { get; set; }

        [Display(Name = "Imagen principal")]
        public HttpPostedFileBase Image { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Imagen preliminar")]
        public HttpPostedFileBase ImagePreview { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Diseño")]
        public string Layout { get; set; }
        public SelectList LayoutList = new SelectList(
                new List<SelectListItem>
                    {
                        new SelectListItem { Selected = false, Text = ImageLayoutText.IMAGE_TOP, Value = ImageLayout.IMAGE_TOP },
                        new SelectListItem { Selected = false, Text = ImageLayoutText.IMAGE_LEFT, Value = ImageLayout.IMAGE_LEFT },
                        new SelectListItem { Selected = true, Text = ImageLayoutText.IMAGE_RIGHT, Value = ImageLayout.IMAGE_RIGHT },
                        new SelectListItem { Selected = false, Text = ImageLayoutText.IMAGE_BOTTOM, Value = ImageLayout.IMAGE_BOTTOM },
                        new SelectListItem { Selected = false, Text = ImageLayoutText.IMAGE_NONE, Value = ImageLayout.IMAGE_NONE }
                    }, "Value", "Text");

        [Display(Name = "Notas")]
        public string Notes { get; set; }
    }

    public class EventEditViewModel
    {
        [Required(ErrorMessage = "Requerido")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Titulo")]
        public string Title { get; set; }

        [Display(Name = "Descripcion")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Publicado")]
        public bool IsPublished { get; set; }

        [AllowHtml]
        [Display(Name = "Contenido")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Horario")]
        public string Timetable { get; set; }

        [Display(Name = "Descripcion de horarios")]
        public string TimetableNotes { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Ubicacion")]
        public string Location { get; set; }

        [Display(Name = "Descripcion de ubicacion")]
        public string LocationNotes { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Contacto")]
        public string Contact { get; set; }

        [Display(Name = "Descripcion de contacto")]
        public string ContactNotes { get; set; }

        public string FilePath { get; set; }

        [Display(Name = "Archivo descargable")]
        public HttpPostedFileBase File { get; set; }

        [Display(Name = "Descripcion de archivo descargable")]
        public string FileNotes { get; set; }

        [Display(Name = "Remover")]
        public bool FileRemove { get; set; }

        // Just to show the image
        public string ImagePath { get; set; }

        [Display(Name = "Imagen principal")]
        public HttpPostedFileBase Image { get; set; }

        [Display(Name = "Remover")]
        public bool ImageRemove { get; set; }

        public string ImagePreviewPath { get; set; }

        [Display(Name = "Imagen preliminar")]
        public HttpPostedFileBase ImagePreview { get; set; }

        [Display(Name = "Remover")]
        public bool ImagePreviewRemove { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Diseño")]
        public string Layout { get; set; }
        public SelectList LayoutList = new SelectList(
                new List<SelectListItem>
                    {
                        new SelectListItem { Selected = false, Text = ImageLayoutText.IMAGE_TOP, Value = ImageLayout.IMAGE_TOP },
                        new SelectListItem { Selected = false, Text = ImageLayoutText.IMAGE_LEFT, Value = ImageLayout.IMAGE_LEFT },
                        new SelectListItem { Selected = false, Text = ImageLayoutText.IMAGE_RIGHT, Value = ImageLayout.IMAGE_RIGHT },
                        new SelectListItem { Selected = false, Text = ImageLayoutText.IMAGE_BOTTOM, Value = ImageLayout.IMAGE_BOTTOM },
                        new SelectListItem { Selected = false, Text = ImageLayoutText.IMAGE_NONE, Value = ImageLayout.IMAGE_NONE }
                    }, "Value", "Text");


        [Display(Name = "Notas")]
        public string Notes { get; set; }
    }
}