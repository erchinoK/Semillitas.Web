using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Semillitas.Web.Models.ViewModels
{
    public class PaymentListItem
    {
        public int ID { get; set; }

        public decimal Amount { get; set; }

        public string Type { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public string PayingMonth { get; set; }

        public bool IsPaid { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ExpirationDate { get; set; }

        public string Notes { get; set; }
    }

    public class PaymentAdminViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Required")]
        public int EnrollmentID { get; set; }

        public Enrollment Enrollment { get; set; }
        //[Required(ErrorMessage = "Required")]
        //public string Type { get; set; }        // Check ConfigVariables.cs (e.g. PAYMENT_REGISTRATION)
        //public SelectList TypeList = new SelectList(
        //        new List<SelectListItem>
        //            {
        //                new SelectListItem { Selected = false, Text = PaymentType.PAYMENT_REGISTRATION, Value = PaymentType.PAYMENT_REGISTRATION },
        //                new SelectListItem { Selected = false, Text = PaymentType.PAYMENT_FEE, Value = PaymentType.PAYMENT_FEE },
        //                new SelectListItem { Selected = false, Text = PaymentType.PAYMENT_RENEW, Value = PaymentType.PAYMENT_RENEW },
        //                new SelectListItem { Selected = false, Text = PaymentType.PAYMENT_OTHER, Value = PaymentType.PAYMENT_OTHER},
        //            }, "Value", "Text");

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Required")]
        public string PaymentMethod { get; set; }      // Check ConfigVariables.cs (e.g. METHOD_CASH)
        public SelectList MethodList = new SelectList(
                new List<SelectListItem>
                    {
                        new SelectListItem { Selected = false, Text = Models.PaymentMethod.METHOD_CASH, Value = Models.PaymentMethod.METHOD_CASH },
                        new SelectListItem { Selected = false, Text = Models.PaymentMethod.METHOD_DEBITCARD, Value = Models.PaymentMethod.METHOD_DEBITCARD },
                        new SelectListItem { Selected = false, Text = Models.PaymentMethod.METHOD_CREDITCARD, Value = Models.PaymentMethod.METHOD_CREDITCARD},
                    }, "Value", "Text");
        
        public virtual IList<PaymentListItem> AllPayments { get; set; }

        //[Range(typeof(bool), "true", "true", ErrorMessage = "You gotta tick the box!")]
        public bool PayingRegistration { get; set; }
        //[Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than 0")]
        [DataType(DataType.Currency)]
        public decimal AmountRegistration { get; set; }
        public string NotesRegistration { get; set; }

        public bool PayingMonth1 { get; set; }
        public decimal AmountMonth1 { get; set; }
        public string NotesMonth1 { get; set; }

        public bool PayingMonth2 { get; set; }
        public decimal AmountMonth2 { get; set; }
        public string NotesMonth2 { get; set; }

        public bool PayingMonth3 { get; set; }
        public decimal AmountMonth3 { get; set; }
        public string NotesMonth3 { get; set; }

        public bool PayingMonth4 { get; set; }
        public decimal AmountMonth4 { get; set; }
        public string NotesMonth4 { get; set; }

        public bool PayingMonth5 { get; set; }
        public decimal AmountMonth5 { get; set; }
        public string NotesMonth5 { get; set; }

        public bool PayingMonth6 { get; set; }
        public decimal AmountMonth6 { get; set; }
        public string NotesMonth6 { get; set; }

        public bool PayingMonth7 { get; set; }
        public decimal AmountMonth7 { get; set; }
        public string NotesMonth7 { get; set; }

        public bool PayingMonth8 { get; set; }
        public decimal AmountMonth8 { get; set; }
        public string NotesMonth8 { get; set; }

        public bool PayingMonth9 { get; set; }
        public decimal AmountMonth9 { get; set; }
        public string NotesMonth9 { get; set; }

        public bool PayingMonth10 { get; set; }
        public decimal AmountMonth10 { get; set; }
        public string NotesMonth10 { get; set; }

        public bool PayingMonth11 { get; set; }
        public decimal AmountMonth11 { get; set; }
        public string NotesMonth11 { get; set; }

        public bool PayingMonth12 { get; set; }
        public decimal AmountMonth12 { get; set; }
        public string NotesMonth12 { get; set; }

        public bool PayingOther { get; set; }
        public decimal AmountOther { get; set; }
        public string NotesOther { get; set; }
    }

}