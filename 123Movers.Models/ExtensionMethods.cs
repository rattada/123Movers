using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

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


        public static int? IntNullOrEmptyReturn0(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return Convert.ToInt32(value);
            }
            return 0;
        }

        public static string TernaryLocalLong(this int? value)
        {
            if (value == Constants.LOCAL)
            {
                return Constants.LOCAL_TEXT;
            }
            return Constants.LONG_TEXT;
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
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value.Trim();
            }
            return null;
        }

        public static string StrReplace(this string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value.Replace("[", "").Replace("]", "").Replace("\"", "");
            }
            return null;
        }
      
        public static int? IfServiceNullLocal(this int? value)
        {
            //if (value == null)
            //{
            //    return Constants.LOCAL;
            //}
            //return value;

            return value ?? Constants.LOCAL;
        }

        public static MvcHtmlString Script(this HtmlHelper helper, string src)
        {
            var builder = new TagBuilder("script");
            builder.MergeAttribute("src",Constants.BASE_JS_FOLDER + src);
            builder.MergeAttribute("type", "text/javascript");
            var tag = MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));

            return tag;
        }

    }
}