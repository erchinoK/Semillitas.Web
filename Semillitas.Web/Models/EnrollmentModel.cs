using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Semillitas.Web.Models
{
    public class Enrollment
    {
        public int ID { get; set; }
        
        public bool IsActive { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ExpirationDate { get; set; }
        
        public string ChildFirstName { get; set; }
        public string ChildLastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime ChildBirthDate { get; set; }
        public bool HasSpecialNeed { get; set; }
        public string SpecialNeedNotes { get; set; }
        public string ChildNotes { get; set; }

        public DateTime CreationDate { get; set; }
        public string CreationUserName { get; set; }
        public DateTime ModifDate { get; set; }
        public string ModifUserName { get; set; }

        public string Notes { get; set; }

        // We use them to create the foreign keys in the db
        public virtual ApplicationUser User { get; set; }
        public virtual Membership Membership { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}