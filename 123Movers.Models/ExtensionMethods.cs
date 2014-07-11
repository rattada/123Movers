using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _123Movers.Models
{
    public static class ExtensionMethods
    {
        public static int? IntNullOrEmpty(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return Convert.ToInt32(value);
            }
            return null;
        }

        public static decimal? DecimalNullOrEmpty(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return Convert.ToDecimal(value);
            }
            return null;
        }
        public static DateTime? DateNullOrEmpty(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return Convert.ToDateTime(value);
            }
            return null;
        }

        public static bool BooleanNullOrEmpty(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return Convert.ToBoolean(value);
            }
            return false;
        }

        public static string TrimNullOrEmpty(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return value.Trim();
            }
            return value;
        }
        public static int? IfServiceNullLocal(this int? value)
        {
            if (value == null)
            {
                return Constants.LOCAL;
            }
            return value;
        }

        public static CompanyModel CompanyInfo(this HtmlHelper helper, string view, CompanyModel _company)
        {
            return _company;

        }
    }
}