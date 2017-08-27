using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Semillitas.Web.Models
{
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
}