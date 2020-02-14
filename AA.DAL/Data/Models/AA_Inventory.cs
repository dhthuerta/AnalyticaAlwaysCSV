using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AA.DAL.Data.Models
{
    public class AA_Inventory
    {
        [Key]
        public int ID { get; set; }
        public string PointOfSale { get; set; }
        public string Product { get; set; }
        public DateTime Date { get; set; }
        public int Stock { get; set; }
    }
}
