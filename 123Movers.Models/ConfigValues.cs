using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel;

namespace _123Movers.Models
{
    public static class ConfigValues
    {
        public static int GetEnumFromDescription(string description, Type enumType)
        {
            foreach (var field in enumType.GetFields())
            {
                DescriptionAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute == null)
                    continue;
                if (attribute.Description == description)
                {
                    return (int)field.GetValue(null);
                }
            }
            return 0;
        }

        //public static string GetDescriptionFromEnumValue(Enum value)
        //{
        //    EnumMemberAttribute attribute = value.GetType()
        //        .GetField(value.ToString())
        //        .GetCustomAttributes(typeof(EnumMemberAttribute), false)
        //        .SingleOrDefault() as EnumMemberAttribute;
        //    return attribute == null ? value.ToString() : attribute.Value;
        //}

        public static List<List<string>> TableToList(DataTable dt)
        {
            List<List<string>> lstTable = new List<List<string>>();
            foreach (DataRow row in dt.Rows)
            {
                List<string> lstRow = new List<string>();
                foreach (var item in row.ItemArray)
                {
                    lstRow.Add(item.ToString().Replace("\r\n", string.Empty));
                }
                lstTable.Add(lstRow);
            }

            return lstTable;

        }

        public static List<List<string>> DataSetToList(DataSet ds)
        {
            List<List<string>> lstTable = new List<List<string>>();
            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataRow row in dt.Rows)
                {
                    List<string> lstRow = new List<string>();
                    foreach (var item in row.ItemArray)
                    {
                        lstRow.Add(item.ToString().Replace("\r\n", string.Empty));
                    }
                    lstRow.Add(dt.TableName);
                    lstTable.Add(lstRow);
                }
            }

            return lstTable;

        }

        public static readonly IEnumerable<KeyValuePair<string, string>> Terms = new List<KeyValuePair<string, string>>()
        {
            //new KeyValuePair<string, string>(TermType.Recurring.ToString(), Convert.ToString(TermType.Recurring)),
            new KeyValuePair<string, string>("Recurring", "0"),
            new KeyValuePair<string, string>("Non Recurring", "1"),
            new KeyValuePair<string, string>("Recurring With Notice", "2"),
        };

        //public static readonly IEnumerable<KeyValuePair<string, string>> Services = new List<KeyValuePair<string, string>>()
        //{
        //    new KeyValuePair<string, string>("Local", "1009"),
        //    new KeyValuePair<string, string>("Long", "1000"),
        //    new KeyValuePair<string, string>("Both", null),
        //};

    }
    
}