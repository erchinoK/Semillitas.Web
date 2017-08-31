using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Semillitas.Web.Models
{
    public class Membership
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsRegistrationRequired { get; set; }
        public int NumberPayments { get; set; }
        public decimal PriceRegistration { get; set; }
        public decimal PriceFee { get; set; }
        public string Duration { get; set; }    // Format "ddmmyy" (e.g. 200000 = 20 days, 000001 = 1 year)
        public int Order { get; set; }          // Indicates the order to show in the website
        public bool IsPublished { get; set; }
        public string Class { get; set; }       // In case it needs to show any extra css
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreationDate { get; set; }
        public string CreationUserName { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ModifDate { get; set; }
        public string ModifUserName { get; set; }
    }

}