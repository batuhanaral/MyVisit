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

        

        public DbSet<Information> Informations { get; set; }
        public DbSet<Contact.Foreing> Foreings { get; set; }
        public DbSet<Contact.Member> Members { get; set; }
        

    }
}