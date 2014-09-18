using System.Data;
using System.Linq;
using System.Collections.Generic;

namespace _123Movers.Models
{
    public static class ConfigValues
    {
        public static List<List<string>> TableToList(DataTable dt)
        {
            return (from DataRow row in dt.Rows select row.ItemArray.Select(item => item.ToString().Replace("\r\n", string.Empty)).ToList()).ToList();
        }

        public static List<List<string>> DataSetToList(DataSet ds)
        {
            var lstTable = new List<List<string>>();
            foreach (DataTable dt in ds.Tables)
            {
                foreach (var lstRow in from DataRow row in dt.Rows select row.ItemArray.Select(item => item.ToString().Replace("\r\n", string.Empty)).ToList())
                {
                    lstRow.Add(dt.TableName);
                    lstTable.Add(lstRow);
                }
            }

            return lstTable;
        }

        public static readonly IEnumerable<KeyValuePair<string, string>> Terms = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("Recurring", "0"),
            new KeyValuePair<string, string>("Non Recurring", "1"),
            new KeyValuePair<string, string>("Recurring With Notice", "2"),
        };
    }
    
}