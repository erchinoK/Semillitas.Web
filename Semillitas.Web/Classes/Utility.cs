﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Semillitas.Web.Classes
{
    public class Utility
    {
        public static DateTime AddDate(DateTime originalDate, string formatToAdd)
        {
            DateTime result = originalDate;

            if (formatToAdd.Length != 6)
                throw new Exception("The date format is not correct.");
            
            int days = Convert.ToInt32(formatToAdd.Substring(0, 2));
            int months = Convert.ToInt32(formatToAdd.Substring(2, 2));
            int years = Convert.ToInt32(formatToAdd.Substring(4, 2));

            result = originalDate.AddDays(days).AddMonths(months).AddYears(years);

            return result;
        }
    }
}