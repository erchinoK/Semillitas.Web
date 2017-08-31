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
        public decimal Amount { get; set; }
        public string Type { get; set; }        // Check ConfigVariables.cs (e.g. PAYMENT_REGISTRATION)
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int RenewalNumber { get; set; }      // Indicates if the user renewed the enrollment and which payment is it for
        public string PaymentMethod { get; set; }      // Check ConfigVariables.cs (e.g. METHOD_CASH)
        public string PayingMonth { get; set; }        // Indicates which month it is paying (0 = registration, 1...12 = fee)
        public DateTime CreationDate { get; set; }
        public string CreationUserName { get; set; }
        public DateTime ModifDate { get; set; }
        public string ModifUserName { get; set; }
        public string Notes { get; set; }

        public virtual Enrollment Enrollment { get; set; }
    }
}