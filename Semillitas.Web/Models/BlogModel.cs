﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Semillitas.Web.Models
{
    public class Blog
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string ImagePreview { get; set; }
        public string Layout { get; set; }
        public bool IsPublished { get; set; }
        public DateTime DatePublishment { get; set; }

        public DateTime CreationDate { get; set; }
        public string CreationUserName { get; set; }
        public DateTime ModifDate { get; set; }
        public string ModifUserName { get; set; }
        public string Notes { get; set; }

    }
}