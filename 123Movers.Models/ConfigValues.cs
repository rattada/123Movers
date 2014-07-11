using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Web.Mvc;

namespace _123Movers.Models
{
    public static class ConfigValues
    {
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

        public static List<SelectListItem> Terms()
        {
            var listOption = new SelectListItem();
            var terms = new List<SelectListItem>();

            listOption = new SelectListItem { Text = "Recurring", Value = "0" };
            terms.Add(listOption);

            listOption = new SelectListItem { Text = "Non Recurring", Value = "1" };
            terms.Add(listOption);

            listOption = new SelectListItem { Text = "Recurring With Notice", Value = "2" };

            terms.Add(listOption);

            return terms;
        }

        public static List<SelectListItem> Services()
        {
            var listOption = new SelectListItem();
            var services = new List<SelectListItem>();

            listOption = new SelectListItem { Text = "Local", Value = "1009" };
            services.Add(listOption);

            listOption = new SelectListItem { Text = "Long", Value = "1000" };
            services.Add(listOption);

            listOption = new SelectListItem { Text = "Both", Value = "999" };
            services.Add(listOption);

            return services;
        }

        public static SelectList DataTableToSelectList(DataTable table, string valueField, string textField)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new SelectListItem()
                {
                    //Text = row[textField].ToString() + "-" + row[valueField].ToString(),
                    Text = row[textField].ToString(),
                    Value = row[valueField].ToString()
                });
            }

            return new SelectList(list, "Value", "Text");
        }

       
    }
    
}