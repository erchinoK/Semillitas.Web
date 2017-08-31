using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Semillitas.Web.Models.ViewModels
{
    public class EnrollmentUserViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Required")]
        public int MembershipID { get; set; }

        public string ChildFirstName { get; set; }
        public string ChildLastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ChildBirthDate { get; set; }
        public bool HasSpecialNeed { get; set; }
        public string SpecialNeedNotes { get; set; }
        public string ChildNotes { get; set; }
    }

    public class EnrollmentAdminViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Required")]
        public string UserID { get; set; }

        [Required(ErrorMessage = "Required")]
        public int MembershipID { get; set; }
        
        public string ChildFirstName { get; set; }
        public string ChildLastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ChildBirthDate { get; set; }
        public bool HasSpecialNeed { get; set; }
        public string SpecialNeedNotes { get; set; }
        public string ChildNotes { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ExpirationDate { get; set; }

        public string Notes { get; set; }
    }
}