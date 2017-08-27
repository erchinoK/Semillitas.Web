using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Semillitas.Web.Models
{
    public class Payment
    {
        public int ID { get; set; }
        public double Amount { get; set; }
        public string Type { get; set; }        // Check ConfigVariables.cs (e.g. PAYMENT_REGISTRATION)
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public string PaymentMethod { get; set; }      // Check ConfigVariables.cs (e.g. METHOD_CASH)
        public string PayingMonth { get; set; }        // Indicates which month it is paying. Check ConfigVariables.cs (e.g. JANUARY)
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreationDate { get; set; }
        public string CreationUserName { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ModifDate { get; set; }
        public string ModifUserName { get; set; }
        public string Notes { get; set; }

        public virtual Enrollment Enrollment { get; set; }
    }
}