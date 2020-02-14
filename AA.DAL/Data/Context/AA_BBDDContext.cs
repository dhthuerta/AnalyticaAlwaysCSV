using AA.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AA.DAL.Data
{
    public class AA_BBDDContext: DbContext
    {
        public DbSet<AA_Inventory> Inventories { get; set; }

        private readonly string connString;
        public AA_BBDDContext(string _connString)
        {
            connString = _connString;
        }
        public AA_BBDDContext(DbContextOptions<AA_BBDDContext> options): base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(connString))
                optionsBuilder.UseSqlServer(connString);
        }
    }
}
