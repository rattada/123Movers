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
            new KeyValuePair<string, string>(TermType.Recurring.ToString(), Convert.ToString(TermType.Recurring)),
            new KeyValuePair<string, string>("Non Recurring", "1"),
            new KeyValuePair<string, string>("Recurring With Notice", "2"),
        };

        //public static readonly IEnumerable<KeyValuePair<string, string>> Services = new List<KeyValuePair<string, string>>()
        //{
        //    new KeyValuePair<string, string>("Local", "1009"),
        //    new KeyValuePair<string, string>("Long", "1000"),
        //    new KeyValuePair<string, string>("Both", null),
        //};

  

        //public static List<SelectListItem> Terms()
        //{
        //    var listOption = new SelectListItem();
        //    var terms = new List<SelectListItem>();

        //    listOption = new SelectListItem { Text = "Recurring", Value = "0" };
        //    terms.Add(listOption);

        //    listOption = new SelectListItem { Text = "Non Recurring", Value = "1" };
        //    terms.Add(listOption);

        //    listOption = new SelectListItem { Text = "Recurring With Notice", Value = "2" };

        //    terms.Add(listOption);

        //    return terms;
        //}

        //public static List<SelectListItem> Services(int? serviceId = null)
        //{
        //    var listOption = new SelectListItem();
        //    var services = new List<SelectListItem>();

        //    if (serviceId == null || serviceId == Constants.LOCAL)
        //    {
        //        listOption = new SelectListItem { Text = "Local", Value = "1009" };

        //        services.Add(listOption);
        //    }
        //    if (serviceId == null || serviceId == Constants.LONG)
        //    {
        //        listOption = new SelectListItem { Text = "Long", Value = "1000" };
        //        services.Add(listOption);
        //    }
        //    if (serviceId == null)
        //    {
        //        listOption = new SelectListItem { Text = "Both", Value = "999" };
        //        services.Add(listOption);
        //    }

        //    return services;
        //}


        //public static SelectList DataTableToSelectList(DataTable table, string valueField, string textField)
        //{
        //    List<SelectListItem> list = new List<SelectListItem>();

        //    foreach (DataRow row in table.Rows)
        //    {
        //        list.Add(new SelectListItem()
        //        {
        //            Text = row[textField].ToString(),
        //            Value = row[valueField].ToString()
        //        });
        //    }

        //    return new SelectList(list, "Value", "Text");
        //}

       
    }
    
}