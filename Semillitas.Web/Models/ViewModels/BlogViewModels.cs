using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Semillitas.Web.Models.ViewModels
{
    public class BlogCreateViewModel
    {
        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Titulo")]
        public string Title { get; set; }

        [Display(Name = "Descripcion")]
        public string Description { get; set; }

        [AllowHtml]
        [Display(Name = "Contenido")]
        public string Content { get; set; }

        [Display(Name = "Imagen principal")]
        public HttpPostedFileBase ImageFile { get; set; }

        [Display(Name = "Imagen preliminar")]
        public HttpPostedFileBase ImagePreviewFile { get; set; }

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
                    new SelectListItem { Selected = false, Text = ImageLayoutText.IMAGE_ONLY, Value = ImageLayout.IMAGE_ONLY },
                    new SelectListItem { Selected = false, Text = ImageLayoutText.IMAGE_NONE, Value = ImageLayout.IMAGE_NONE }
                    }, "Value", "Text");


        [Display(Name = "Notas")]
        public string Notes { get; set; }
    }

    public class BlogEditViewModel
    {
        [Required(ErrorMessage = "Requerido")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Titulo")]
        public string Title { get; set; }

        [Display(Name = "Descripcion")]
        public string Description { get; set; }

        [AllowHtml]
        [Display(Name = "Contenido")]
        public string Content { get; set; }

        [Display(Name = "Publicado")]
        public bool IsPublished { get; set; }

        // Just to show the image
        public string Image { get; set; }

        [Display(Name = "Imagen principal")]
        public HttpPostedFileBase ImageFile { get; set; }

        [Display(Name = "Remover imagen principal")]
        public bool ImageRemove { get; set; }

        // Just to show the image
        public string ImagePreview { get; set; }

        [Display(Name = "Imagen preliminar")]
        public HttpPostedFileBase ImagePreviewFile { get; set; }

        [Display(Name = "Remover imagen preliminar")]
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
                    new SelectListItem { Selected = false, Text = ImageLayoutText.IMAGE_ONLY, Value = ImageLayout.IMAGE_ONLY },
                    new SelectListItem { Selected = false, Text = ImageLayoutText.IMAGE_NONE, Value = ImageLayout.IMAGE_NONE }
                    }, "Value", "Text");


        [Display(Name = "Notas")]
        public string Notes { get; set; }
    }
}