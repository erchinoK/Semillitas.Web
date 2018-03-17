using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Semillitas.Web.Models
{
    public class Slide
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public string ImagePathEsp { get; set; }
        public string ImagePathCat { get; set; }
        public string ImagePathEng { get; set; }

        public int Position { get; set; }
        public bool IsPublished { get; set; }

        [AllowHtml]
        public string Html { get; set; }

        public DateTime? PublishDate { get; set; }

        public DateTime CreationDate { get; set; }
        public string CreationUserName { get; set; }
        public DateTime ModifDate { get; set; }
        public string ModifUserName { get; set; }
        public string Notes { get; set; }
    }
}