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

        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [AllowHtml]
        [Required]
        public string Content { get; set; }

        [Required]
        public HttpPostedFileBase ImageFile { get; set; }

        [Required]
        public string Layout { get; set; }
        public bool IsPublished { get; set; }

        public string Notes { get; set; }

        public SelectList ImageLayoutList = new SelectList(
                new List<SelectListItem>
                    {
                        new SelectListItem { Selected = false, Text = ImageLayout.IMAGE_TOP, Value = ImageLayout.IMAGE_TOP },
                        new SelectListItem { Selected = true, Text = ImageLayout.IMAGE_LEFT, Value = ImageLayout.IMAGE_LEFT },
                        new SelectListItem { Selected = false, Text = ImageLayout.IMAGE_RIGHT, Value = ImageLayout.IMAGE_RIGHT },
                        new SelectListItem { Selected = false, Text = ImageLayout.IMAGE_BOTTOM, Value = ImageLayout.IMAGE_BOTTOM },
                    }, "Value", "Text");
    }
}