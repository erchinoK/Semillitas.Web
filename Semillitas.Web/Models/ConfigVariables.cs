using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Semillitas.Web.Models
{

    public enum test { Action, Drama, Comedy, Science_Fiction };

    public class RoleNames
    {
        public const string ROLE_ADMINISTRATOR = "ADMINISTRATOR";
        public const string ROLE_OPERATOR = "OPERATOR";
        public const string ROLE_USER = "USER";
    }

    // Try to use the codes in the db first
    public class MembershipCodes
    {
        public const string MEMBERSHIP_ANNUAL = "ANNUAL";
        public const string MEMBERSHIP_MONTHLY = "MONTHLY";
        public const string MEMBERSHIP_DAILY = "DAILY";
    }

    public class PaymentType
    {
        public const string PAYMENT_REGISTRATION = "REGISTRATION";
        public const string PAYMENT_FEE = "FEE";
        public const string PAYMENT_RENEW = "RENEW";
        public const string PAYMENT_OTHER = "OTHER";
    }

    public class DurationInDays
    {
        public const int DURATION_YEAR = 365;
        public const int DURATION_MONTH = 30;
        public const int DURATION_DAY = 1;
    }

    public class PaymentMethod
    {
        public const string METHOD_CASH = "CASH";
        public const string METHOD_DEBITCARD = "DEBIT_CARD";
        public const string METHOD_CREDITCARD = "CREDIT_CARD";
    }

    //public class Month
    //{
    //    public int ID;
    //    public string Name;        
    //}

    //public class PaymentMonth
    //{
    //    public Month January = new Month { ID = 1, Name = "Enero"};
    //    public Month February = new Month { ID = 2, Name = "Febrero" };
    //    public Month March = new Month { ID = 3, Name = "Marzo" };
    //    public Month April = new Month { ID = 4, Name = "Abril" };
    //    public Month May = new Month { ID = 5, Name = "Mayo" };
    //    public Month June = new Month { ID = 6, Name = "Junio" };
    //    public Month July = new Month { ID = 7, Name = "Julio" };
    //    public Month August = new Month { ID = 8, Name = "Agosto" };
    //    public Month September = new Month { ID = 9, Name = "Septiembre" };
    //    public Month October = new Month { ID = 10, Name = "Octubre" };
    //    public Month November = new Month { ID = 11, Name = "Noviembre" };
    //    public Month December = new Month { ID = 12, Name = "Diciembre" };
    //}
}