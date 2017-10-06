using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Semillitas.Web.Models
{
    public class Marketing
    {
        public int ID { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string Notes { get; set; }
    }
}