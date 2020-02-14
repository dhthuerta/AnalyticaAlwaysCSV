using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Transactions;

namespace AA.CrossDomain
{
    public static class Helper
    {
        public static DataTable ToDataTable(string data)
        {
            DataTable dt = new DataTable();

            string[] tableData = data.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var col = from cl in tableData[0].Split(Constants.CSV_SEPARATOR.ToCharArray())
                      select new DataColumn(cl);
            dt.Columns.AddRange(col.ToArray());

            (from st in tableData.Skip(1)
             select dt.Rows.Add(st.Split(Constants.CSV_SEPARATOR.ToCharArray()))).ToList();

            return dt;
        }

        public static TransactionScope CreateTransactionScope(int secsTimeout)
        {
            var transactionOptions = new TransactionOptions() { Timeout = TimeSpan.FromSeconds(secsTimeout) };
            return new TransactionScope(TransactionScopeOption.Required, transactionOptions);
        }
    }
}
