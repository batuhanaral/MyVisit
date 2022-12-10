using Microsoft.AspNet.Identity.EntityFramework;
using ProjeA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjeA.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public List<PersonelShortInfofmation> personelShortInfofmations { get; set; }
    }
}