using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjeA.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("dbConnection")   
        {

        }

        public DbSet<PersonelShortInfofmation> PersonelShortInfos { get; set; }
    }
}