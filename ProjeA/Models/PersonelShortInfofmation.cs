using ProjeA.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace ProjeA.Models
{
    public class PersonelShortInfofmation
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public string Surname { get; set; }
        public String CompanyName { get; set; }
        public String Motto { get; set; }
        public string PhoneNumber { get; set; }
        public string PhotoAdress { get; set; }
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}