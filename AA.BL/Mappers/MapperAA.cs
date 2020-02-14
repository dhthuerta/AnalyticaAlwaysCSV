using AA.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AA.BL.Mappers
{
    public static class MapperAA
    {
        public static List<AA_Inventory> ToAA_Inventory(DataTable dt)
        {
            var list = new List<AA_Inventory>();

            foreach (DataRow row in dt.Rows)
                list.Add(RowToAA_Inventory(row));

            return list;
        }

        private static AA_Inventory RowToAA_Inventory(DataRow row)
        {
            var cPoS = (string)row["PointOfSale"];
            var cProd = (string)row["Product"];
            var cDate = Convert.ToDateTime(row["Date"]);
            var cStock = Convert.ToInt32(row["Stock"]);

            var inventory = new AA_Inventory()
            {
                PointOfSale = cPoS,
                Product = cProd,
                Date = cDate,
                Stock = cStock
            };

            return inventory;
        }
    }
}
